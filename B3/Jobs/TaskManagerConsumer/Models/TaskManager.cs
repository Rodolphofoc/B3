namespace TaskManagerConsumer.Models
{
    public class TaskManager : Entity
    {
        public string Description { get; set; }
        public StatusTask Status { get; set; }
    }

    public enum StatusTask
    {
        Pending,
        InProgress,
        Completed,
        Canceled
    }
}
