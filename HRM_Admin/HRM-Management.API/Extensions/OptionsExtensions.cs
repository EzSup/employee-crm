using HRM_Management.API.Options;
using HRM_Management.Dal.Options;
using HRM_Management.DalS3.Options;
using HRM_Management.Infrastructure.Options;
namespace HRM_Management.API.Extensions
{
    public static class OptionsExtensions
    {
        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(configuration.GetSection("ConnectionStrings"));
            services.Configure<TokenOptions>(configuration.GetSection("Token"));
            services.Configure<S3Options>(configuration.GetSection("AWS"));
            services.Configure<HRM_TgBotApiOptions>(configuration.GetSection("HRM-TgBotApi"));
            services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
            services.Configure<OpenTelemetryOptions>(configuration.GetSection("OpenTelemetry"));
            services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQ"));

            return services;
        }
    }
}
