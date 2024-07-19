using Applications.Interfaces.Repository;
using Applications.Interfaces.Service;
using Domain;
using Domain.Domain;
using MediatR;
using System.Net;

namespace Applications.Finance.Commands.Handlers
{
    public class AddTaskManagerCommandHandler : IRequestHandler<AddTaskManagerCommand, Response>
    {
        private readonly IResponse _response;
        private readonly ITaskManagerService _taskManagerService;
        private readonly ITaskManagerRepository _taskManagerRepository;


        public AddTaskManagerCommandHandler(IResponse response, ITaskManagerService financeService, ITaskManagerRepository metaRepository, IUnitOfWork unitOfWork)
        {
            _response = response;
            _taskManagerService = financeService;
            _taskManagerRepository = metaRepository;
        }

        public async Task<Response> Handle(AddTaskManagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new TaskManager()
                {
                    Description = request.Description,
                    Status = request.Status,
                    CreatedAt = DateTime.Now
                    
                };

                await _taskManagerService.SendMessage(entity);

                return await _response.CreateSuccessResponseAsync(null, string.Empty);

            }
            catch (Exception)
            {
                return await _response.CreateErrorResponseAsync(null, HttpStatusCode.InternalServerError);
            }
        }
    }
}
