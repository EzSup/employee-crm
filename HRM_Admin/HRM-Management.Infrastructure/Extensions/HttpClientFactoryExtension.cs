using HRM_Management.Infrastructure.Options;
using HRM_Management.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static HRM_Management.Core.Helpers.Constants;

namespace HRM_Management.Infrastructure.Extensions
{
    public static class HttpClientFactoryExtension
    {
        public static IServiceCollection AddHttpClientFactory(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddHttpClient(TgBotHttpClientName, (sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<HRM_TgBotApiOptions>>().Value;

                client.BaseAddress = new Uri(options.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(120);
            }).AddHttpMessageHandler<AuthDelegationHandler>();
            return services;
        }
    }
}
