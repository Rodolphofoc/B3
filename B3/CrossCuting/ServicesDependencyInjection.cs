using Applications.Interfaces.Repository;
using Applications.Interfaces.Service;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services.Finance;
using Infrastructure.Services.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace CrossCuting
{
    public static class ServicesDependencyInjection
    {
        private const string applicationProjectName = "Applications";


        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.Load(applicationProjectName);

            services.AddDbContext<B3Context>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                 o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Carregar as configurações do RabbitMQ a partir do appsettings.json
            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));

            // Registrar o TaskManagerService e a interface ITaskManagerService
            services.AddSingleton<ITaskManagerService, TaskManagerService>();

            services.AddAutoMapper(assembly);

            services.AddMediatr();
            return services;

        }

        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITaskManagerRepository, TaskManagerRepository>();


            return services;
        }

        private static void AddMediatr(this IServiceCollection services)
        {
            var assembly = Assembly.Load(applicationProjectName);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.Load(applicationProjectName)));
        }
    }
}
