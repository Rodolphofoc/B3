using Domain;
using Domain.Domain;
using MediatR;

namespace Applications.Finance.Commands
{
    public class UpdateTaskManagerCommand  : IRequest<Response>
    {
        private Guid IntegrationId { get; set; }
        public string Description { get; set; }

        public StatusTask Status { get; set; }

        public void SetId(Guid id)
        {
            IntegrationId = id;
        }   

        public Guid GetId()
        {
            return IntegrationId;
        }
    }
}
