namespace Infrastructure.Services.Settings
{
    public class RabbitMQSettings
    {
        public string Hostname { get; set; }
        public string QueueName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
