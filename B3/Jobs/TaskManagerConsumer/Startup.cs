using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerConsumer.Repository.Settings;
using TaskManagerConsumer.Settings;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configuração de DatabaseSettings
        services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));

        // Registro de outros serviços
        services.AddSingleton<RabbitMQSettings>(sp =>
        {
            return new RabbitMQSettings
            {
                Hostname = "localhost",
                UserName = "guest",
                Password = "guest",
                QueueName = "myQueue"
            };
        });

        services.AddHostedService<QueueConsumerBackgroundService>();
    }
}
