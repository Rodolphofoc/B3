using Dapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Data.SqlClient;
using System.Text;
using TaskManagerConsumer.Interface;
using TaskManagerConsumer.Models;
using TaskManagerConsumer.Settings;

public class QueueConsumerService: IQueueConsumerService
{
    private readonly RabbitMQSettings _rabbitMQSettings;
    private readonly SqlConnection _sqlConnection;
    private readonly ConnectionFactory factory;

    public QueueConsumerService(IOptions<RabbitMQSettings> rabbitMQOptions, SqlConnection sqlConnection)
    {
        _rabbitMQSettings = rabbitMQOptions.Value;
        _sqlConnection = sqlConnection;
        factory = new ConnectionFactory
        {
            HostName = _rabbitMQSettings.Hostname,
            UserName = _rabbitMQSettings.UserName,
            Password = _rabbitMQSettings.Password
        };
    }


    public async Task StartConsuming()
    {
        Console.WriteLine("Starting consumer...");

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _rabbitMQSettings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        Console.WriteLine("Connection on rabbitMq OK....");

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"[x] Received {message}");

            try
            {

                var messageModel = JsonConvert.DeserializeObject<TaskManager>(message);
                if (messageModel.IntegrationId != Guid.Empty)
                {
                    var existingMessage = await GetMessage(messageModel.IntegrationId);
                    if (existingMessage != null)
                    {
                        await UpdateMessage(messageModel);
                        Console.WriteLine("Updated Task");
                    }
                }
                else
                {
                    await SaveMessageToDatabaseAsync(messageModel);
                    Console.WriteLine("Inserted Task");

                }

                Console.WriteLine("Remove message Task from queue");
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                Console.WriteLine("Finish process to save message...");

                Console.WriteLine("Listen rabbitmq...");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Internal error in message {message} with exception : {ex.Message}" );

               throw ex;
            }


        };

        channel.BasicConsume(queue: _rabbitMQSettings.QueueName, autoAck: false, consumer: consumer);

        Console.WriteLine("Consumer started. Press [enter] to exit.");
        Console.ReadLine();
    }


    private async Task SaveMessageToDatabaseAsync(TaskManager message)
    {
        var query = @"INSERT INTO [dbo].[TaskManager]
                       ([Description]
                       ,[Status]
                       ,[Deleted]
                       ,[IntegrationId])
                 VALUES
                       (@Description, @Status, @Deleted, newid())";

        Console.WriteLine("message save");


        await _sqlConnection.ExecuteAsync(query, message);
    }

    public async Task<TaskManager> GetMessage(Guid integrationId)
    {
        var query = @"SELECT [Id]
                          ,[Description]
                          ,[Status]
                          ,[Deleted]
                          ,[IntegrationId]
                      FROM [B3].[dbo].[TaskManager]
                        WHERE IntegrationId = @IntegrationId";

        return await _sqlConnection.QueryFirstOrDefaultAsync<TaskManager>(query, new { IntegrationId = integrationId });
    }


    public async Task UpdateMessage(TaskManager message)
    {
        var query = @"UPDATE [dbo].[TaskManager]
                       SET [Description] = @Description
                          ,[Status] = @Status
                          ,[Deleted] = @Deleted
                     WHERE IntegrationId = @IntegrationId";

        await _sqlConnection.ExecuteAsync(query, message);
    }
}
