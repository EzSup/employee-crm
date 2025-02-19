using Microsoft.Extensions.DependencyInjection;

namespace HRM_Management.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructureExtensions(this IServiceCollection services)
        {
            services.AddQuartzConfiguration();
            services.AddHttpClientFactory();
            services.AddTemplateFileGenerationService();
            services.AddMessageServiceFactory();
            services.AddNotificationMessageProvider();

            return services;
        }
    }
}
