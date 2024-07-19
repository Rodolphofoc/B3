using Applications.Interfaces.Repository;
using Applications.Interfaces.Service;
using Domain;
using MediatR;
using System.Net;

namespace Applications.Finance.Commands.Handlers
{
    public class UpdateTaskManagerCommandHandler : IRequestHandler<UpdateTaskManagerCommand, Response>  
    {
        private readonly IResponse _response;
        private readonly ITaskManagerService _taskManagerService;
        private readonly ITaskManagerRepository _taskManagerRepository;


        public UpdateTaskManagerCommandHandler(IResponse response, ITaskManagerService financeService, ITaskManagerRepository metaRepository, IUnitOfWork unitOfWork)
        {
            _response = response;
            _taskManagerService = financeService;
            _taskManagerRepository = metaRepository;
        }

        public async Task<Response> Handle(UpdateTaskManagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _taskManagerRepository.FindByIdAsync(request.GetId());

                if (entity is null)
                    return await _response.CreateErrorResponseAsync(HttpStatusCode.NotFound);

                entity.LastModified = DateTime.Now;
                entity.Description = request.Description;
                entity.Status = request.Status;

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
