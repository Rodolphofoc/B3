using Applications.Interfaces.Repository;
using Applications.Interfaces.Service;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Applications.Finance.Commands.Handlers
{
    public class DeleteTaskManagerCommandHandler : IRequestHandler<DeleteTaskManagerCommand, Response>
    {
        private readonly IResponse _response;
        private readonly ITaskManagerService _taskManagerService;
        private readonly ITaskManagerRepository _taskManagerRepository;
        private readonly ILogger<DeleteTaskManagerCommandHandler> _logger;

        public DeleteTaskManagerCommandHandler(IResponse response, ITaskManagerService financeService, ITaskManagerRepository taskRepository, ILogger<DeleteTaskManagerCommandHandler> logger)
        {
            _response = response;
            _taskManagerService = financeService;
            _taskManagerRepository = taskRepository;
            _logger = logger;
        }


        public async Task<Response> Handle(DeleteTaskManagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start process to delete task in rabbitMq");

                var entity = await _taskManagerRepository.FindByIdAsync(request.GetId());

                if (entity is null)
                    return await _response.CreateErrorResponseAsync(HttpStatusCode.NotFound);
                entity.Deleted = true;

                _logger.LogInformation("Start process to send task in rabbitMq");

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
