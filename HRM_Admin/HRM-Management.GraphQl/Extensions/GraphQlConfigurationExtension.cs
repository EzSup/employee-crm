using HRM_Management.Dal;
using HRM_Management.GraphQl.Queries;
using HRM_Management.GraphQl.Types;
using Microsoft.Extensions.DependencyInjection;

namespace HRM_Management.GraphQl.Extensions
{
    public static class GraphQLConfigurationExtension
    {
        public static IServiceCollection AddGraphQlConfiguration(this IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddAuthorization()
                .ModifyCostOptions(options =>
                {
                    options.MaxFieldCost = 10_000;
                    options.MaxTypeCost = 10_000;
                })
                .RegisterDbContextFactory<AppDbContext>()
                .AddQueryType<GraphQlQuery>()
                .AddType<PersonType>()
                .AddType<HubType>()
                .AddSorting()
                .AddFiltering()
                .AddProjections();
            return services;
        }
    }
}
