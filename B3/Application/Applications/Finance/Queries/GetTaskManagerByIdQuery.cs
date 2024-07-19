using Domain;
using MediatR;

namespace Applications.Finance.Queries
{
    public class GetTaskManagerByIdQuery : IRequest<Response>
    {
        public Guid IntegrationId { get; set; }

        public void SetId(Guid id)
        {
            IntegrationId = id;
        }
    }
}
