using Domain;
using MediatR;

namespace Applications.Finance.Commands
{
    public class DeleteTaskManagerCommand : IRequest<Response>
    {
        private Guid IntegrationId { get; set; }

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
