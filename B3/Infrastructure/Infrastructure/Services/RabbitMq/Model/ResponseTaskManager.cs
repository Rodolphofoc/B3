using Domain.Domain;

namespace Infrastructure.Services.Finance.Model
{
    public class ResponseTaskManager
    {
        public Guid IntegrationId { get; set; }
        public string Description { get; set; }
        public StatusTask Status { get; set; }

    }


}

