using HRM_Management.Core.Services;
using HRM_Management.Infrastructure.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace HRM_Management.Infrastructure.Extensions
{
    public static class TemplateFileGenerationExtension
    {
        public static IServiceCollection AddTemplateFileGenerationService(this IServiceCollection services)
        {
            services.AddScoped<IMailTemplateFileGenerationHelper, MailTemplateFileGenerationHelper>();
            return services;
        }
    }
}
