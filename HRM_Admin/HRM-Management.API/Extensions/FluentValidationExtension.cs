using FluentValidation;
using FluentValidation.AspNetCore;
using HRM_Management.API.Validators;

namespace HRM_Management.API.Extensions
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddFluentValidationWithConfigurations(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<ApplicationSubmitValidator>();
            return services;
        }
    }
}
