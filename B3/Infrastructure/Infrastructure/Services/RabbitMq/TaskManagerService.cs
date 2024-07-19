using Applications.Interfaces.Service;
using Infrastructure.Services.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services.Finance
{
    public class TaskManagerService : ITaskManagerService 
    {
        private readonly RabbitMQSettings _settings;
        private IConnection _connection;

        public TaskManagerService(IOptions<RabbitMQSettings> settings)
        {
            _settings = settings.Value;
            CreateConnection();
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _settings.Hostname,
                    UserName = _settings.UserName,
                    Password = _settings.Password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        public async Task SendMessage<T>(T message)
        {
            if (_connection == null)
                CreateConnection();
 

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: _settings.QueueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: "",
                                     routingKey: _settings.QueueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($" [x] Sent {message}");
            }
        }
    }
}
