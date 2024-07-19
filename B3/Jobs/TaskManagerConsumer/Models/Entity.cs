namespace TaskManagerConsumer.Models
{
    public class Entity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public bool Deleted { get; set; }

        public string? LastModifiedBy { get; set; }

        public Guid IntegrationId { get; set; }

        protected Entity()
        {
        }
    }
}
