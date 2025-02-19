using HRM_Management.Bll.Services;
using HRM_Management.Core.Interfaces;
using HRM_Management.Core.Services;
using HRM_Management.GraphQl.Services;
using HRM_Management.Infrastructure.Services;
namespace HRM_Management.API.Extensions
{
    public static class ServicesConfigurationExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<IPersonsService, PersonsService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IChildService, ChildService>();
            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<IHubService, HubService>();

            services.AddScoped<ITokenProviderService, TokenProviderService>();
            services.AddScoped<AuthDelegationHandler>();

            services.AddScoped<IGraphQLFetchingService, GraphQLFetchingService>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddSingleton<ISchedulerService, QuartzSchedulerService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            return services;
        }
    }
}
