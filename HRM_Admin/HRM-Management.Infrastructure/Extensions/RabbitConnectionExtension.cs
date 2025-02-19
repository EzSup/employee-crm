using HRM_Management.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
namespace HRM_Management.Infrastructure.Extensions
{
    public static class RabbitConnectionExtension
    {
        public static IServiceCollection AddRabbitConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("RabbitMQ").Get<RabbitMQOptions>();

            var factory = new ConnectionFactory
            {
                HostName = options.Host,
                Port = options.Port,
                UserName = options.Username,
                Password = options.Password
            };
            factory.ClientProvidedName = "API-Producer";

            //REVIEW: I know this way (calling async methods in sync ones) is not quiet correct,
            // but in latest library version there was removed a sync connection creating method,
            // so I did it this way. If there are any better solutions of this problem You know,
            // please, tell me :)
            Task.Run(async () =>
            {
                var connection = await factory.CreateConnectionAsync();
                services.AddSingleton<IConnection>(connection);
            }).Wait();
            
            return services;
        }
    }
}
