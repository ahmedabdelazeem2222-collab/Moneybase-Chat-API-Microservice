using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoneyBase.Support.Application.Interfaces;
using MoneyBase.Support.Application.Services;
using MoneyBase.Support.Infrastructure.Configuration;
using MoneyBase.Support.Infrastructure.MessageBus;
using MoneyBase.Support.Infrastructure.Persistence.Repositories;
using System;

namespace MoneyBase.Support.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMoneyBaseServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<RabbitSettings>(configuration.GetSection("Rabbit"));

            // Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();

            // Application services
            services.AddScoped<IChatAssignmentService, ChatAssignmentService>();

            // RabbitMQ producer
            services.AddSingleton<IChatProducer>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<RabbitSettings>>().Value;
                var logger = sp.GetRequiredService<ILogger<RabbitMqChatProducer>>();
                return new RabbitMqChatProducer(options.Host, options.User, options.Pass, logger);
            });

            return services;
        }
        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
