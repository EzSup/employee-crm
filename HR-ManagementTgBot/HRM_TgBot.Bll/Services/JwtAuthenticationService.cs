using System.IdentityModel.Tokens.Jwt;
using System.Text;
using HRM_TgBot.Bll.Options;
using HRM_TgBot.Core.ServicesInterfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HRM_TgBot.Bll.Services;

public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly AuthenticationOptions _authOptions;

    public JwtAuthenticationService(IOptions<AuthenticationOptions> authOptions)
    {
        _authOptions = authOptions.Value;
    }

    public async Task<string> LogInAsync(string apiLoginKey)
    {
        if (!string.Equals(apiLoginKey, _authOptions.LogInKey, StringComparison.Ordinal))
            throw new UnauthorizedAccessException("Invalid login key.");
        var token = GenerateJwtToken();
        return await Task.FromResult(token);
    }

    private string GenerateJwtToken()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _authOptions.Issuer,
            _authOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(_authOptions.ExpirationTimeMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}