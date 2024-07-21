using Applications.Interfaces.Repository;
using Domain;
using MediatR;
using System.Net;

namespace Applications.Finance.Queries.Handlers
{
    public class GetTaskManagerQueryHandler :  IRequestHandler<GetTaskManagerQuery, Response>
    {
        private readonly IResponse _response;
        private readonly ITaskManagerRepository _taskRepository;

        public GetTaskManagerQueryHandler(IResponse response, ITaskManagerRepository taskRepository)
        {
            _response = response;
            _taskRepository = taskRepository;
        }

        public async Task<Response> Handle(GetTaskManagerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _taskRepository.GetAll().Where(x => !x.Deleted);
                return await _response.CreateSuccessResponseAsync(result.ToList(), string.Empty);
            }
            catch (Exception)
            {
                return await _response.CreateErrorResponseAsync(HttpStatusCode.InternalServerError) ;
            }
        }
    }
}
