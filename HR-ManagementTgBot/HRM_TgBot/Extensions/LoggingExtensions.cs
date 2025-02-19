using HRM_TgBot.Options;
using Serilog;
using Serilog.Sinks.OpenTelemetry;

namespace HRM_TgBot.Extensions;

public static class LoggingExtensions
{
    public static IServiceCollection AddLoggingServices(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("OpenTelemetry").Get<OpenTelemetryOptions>();
        if (options is not null && !String.IsNullOrEmpty(options.ApiKey))
        {
            Log.Logger = new LoggerConfiguration()
                         .Enrich.FromLogContext()
                         .WriteTo.Console()
                         .WriteTo.OpenTelemetry(x =>
                         {
                             x.Endpoint = options!.BaseUrl;
                             x.Protocol = OtlpProtocol.HttpProtobuf;
                             x.Headers = new Dictionary<string, string>
                             {
                                 ["X-Seq-ApiKey"] = options.ApiKey!
                             };
                         })
                         .CreateLogger();
        }

        services.AddSerilog();
        return services;
    }
}