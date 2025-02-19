using HRM_TgBot.Bll.Extensions;
using HRM_TgBot.Bll.Services;
using HRM_TgBot.Core.ServicesInterfaces;
using HRM_TgBot.Extensions;
using HRM_TgBot.Infrastructure;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HRM_TgBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        builder.Configuration.AddUserSecrets<Program>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.ConfigureProjectOptions(configuration);
        builder.Services.AddHttpClientFactory();
        builder.Services.AddLoggingServices(configuration);
        builder.Services.AddTelegramBotService();
        builder.Services.AddHostedService<RabbitMQHostedService>();

        builder.Services.AddJWTAuthentication(configuration);
        builder.Services.AddSingleton<IJwtAuthenticationService, JwtAuthenticationService>();

        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurations>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}