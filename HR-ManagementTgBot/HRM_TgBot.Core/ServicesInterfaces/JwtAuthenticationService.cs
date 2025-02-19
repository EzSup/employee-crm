namespace HRM_TgBot.Core.ServicesInterfaces;

public interface IJwtAuthenticationService
{
    Task<string> LogInAsync(string ApiLoginKey);
}