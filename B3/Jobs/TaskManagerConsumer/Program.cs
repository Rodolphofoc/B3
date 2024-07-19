using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.IO;
using TaskManagerConsumer.Interface;
using TaskManagerConsumer.Repository.Settings;
using TaskManagerConsumer.Settings;

class Program
{
    static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var environment = context.HostingEnvironment.EnvironmentName ?? "Development";
                config.SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<RabbitMQSettings>(context.Configuration.GetSection("RabbitMQSettings"));
                services.Configure<DatabaseSettings>(context.Configuration.GetSection("DatabaseSettings"));

                services.AddSingleton(serviceProvider =>
                {
                    var databaseSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                    return new SqlConnection(databaseSettings.ConnectionString);
                });

                services.AddSingleton<IQueueConsumerService, QueueConsumerService>();
                services.AddHostedService<QueueConsumerBackgroundService>();
            });
}
