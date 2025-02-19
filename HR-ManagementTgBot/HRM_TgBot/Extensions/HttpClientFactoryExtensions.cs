using HRM_TgBot.Bll.Options;
using Microsoft.Extensions.Options;

namespace HRM_TgBot.Extensions;

public static class HttpClientFactoryExtensions
{
    public static IServiceCollection AddHttpClientFactory(this IServiceCollection services)
    {
        services.AddHttpClient("PersonSubmissionHttpClient", (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<HrmAPIOptions>>().Value;
            client.BaseAddress = new Uri($"{options.BaseUrl}/api/application/");
            client.Timeout = TimeSpan.FromSeconds(120);
        });

        services.AddHttpClient("BlogHttpClient", (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<HrmAPIOptions>>().Value;
            client.BaseAddress = new Uri($"{options.BaseUrl}/api/blog/");
            client.Timeout = TimeSpan.FromSeconds(120);
        });

        return services;
    }
}