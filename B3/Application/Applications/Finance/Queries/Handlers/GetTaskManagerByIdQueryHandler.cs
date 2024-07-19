using Applications.Interfaces.Repository;
using Domain;
using MediatR;
using System.Net;

namespace Applications.Finance.Queries.Handlers
{
    public class GetTaskManagerByIdQueryHandler : IRequestHandler<GetTaskManagerByIdQuery, Response>
    {
        private readonly IResponse _response;
        private readonly ITaskManagerRepository _taskRepository;

        public GetTaskManagerByIdQueryHandler(IResponse response, ITaskManagerRepository taskRepository)
        {
            _response = response;
            _taskRepository = taskRepository;
        }

        public async Task<Response> Handle(GetTaskManagerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    _response.CreateErrorResponseAsync(System.Net.HttpStatusCode.BadRequest);

                var result = await _taskRepository.FindByIdAsync(request.IntegrationId);

                return await _response.CreateSuccessResponseAsync(result, string.Empty);

            }
            catch (Exception)
            {
                return await _response.CreateErrorResponseAsync(HttpStatusCode.InternalServerError);
            }
        }
    }
}
