using Applications.Interfaces.Repository;
using Applications.Interfaces.Service;
using Domain;
using Domain.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Applications.Finance.Commands.Handlers
{
    public class AddTaskManagerCommandHandler : IRequestHandler<AddTaskManagerCommand, Response>
    {
        private readonly IResponse _response;
        private readonly ITaskManagerService _taskManagerService;
        private readonly ILogger<AddTaskManagerCommandHandler> _logger;


        public AddTaskManagerCommandHandler(IResponse response, ITaskManagerService financeService, ILogger<AddTaskManagerCommandHandler> logger )
        {
            _response = response;
            _taskManagerService = financeService;
            _logger = logger;
        }

        public async Task<Response> Handle(AddTaskManagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start process to add new task in rabbitMq");

                var entity = new TaskManager()
                {
                    Description = request.Description,
                    Status = request.Status                    
                };

                _logger.LogInformation("Message send to rabbitMq");

                await _taskManagerService.SendMessage(entity);

                return await _response.CreateSuccessResponseAsync(null, string.Empty);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"something went wrong message : {ex.Message}");
                return await _response.CreateErrorResponseAsync(null, HttpStatusCode.InternalServerError);
            }
        }
    }
}
