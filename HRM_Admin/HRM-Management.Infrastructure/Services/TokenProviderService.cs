using HRM_Management.Core.Services;
using HRM_Management.Infrastructure.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using static HRM_Management.Core.Helpers.HelperMethods;

namespace HRM_Management.Infrastructure.Services
{
    public class TokenProviderService : ITokenProviderService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TokenProviderService> _logger;
        private readonly HRM_TgBotApiOptions _options;

        public TokenProviderService(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory, IOptions<HRM_TgBotApiOptions> options, ILogger<TokenProviderService> logger)
        {
            _memoryCache = memoryCache;
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<string> GetTokenAsync(string httpClientName)
        {
            if (!_memoryCache.TryGetValue(httpClientName, out string? token))
            {
                token = await FetchTokenAsync(httpClientName, _options.BaseUrl);

                _memoryCache.Set(httpClientName, token, TimeSpan.FromMinutes(_options.ExpirationTimeMinutes));
            }
            return token!;
        }

        private async Task<string> FetchTokenAsync(string httpClientName, string defaultUrl)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var content = CreateJsonContent(_options.LogInKey, Encoding.UTF8);
                var response = await client.PostAsync($"{defaultUrl}Authentication/Auth", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error fetching token: {response.StatusCode}");
                }

                var token = await response.Content.ReadAsStringAsync();
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching the token.");
                throw;
            }
        }
    }
}
