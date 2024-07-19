namespace Domain.Domain
{
    public class TaskManager : Entity
    {
        public string Description { get; set; }
        public StatusTask Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastModified { get; set; }

        public bool Deleted { get; set; }
    }
}
