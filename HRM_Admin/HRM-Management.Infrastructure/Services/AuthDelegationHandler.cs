using HRM_Management.Core.Services;
using System.Net.Http.Headers;
using static HRM_Management.Core.Helpers.Constants;

namespace HRM_Management.Infrastructure.Services
{
    public class AuthDelegationHandler : DelegatingHandler
    {
        private readonly ITokenProviderService _tokenProviderService;

        public AuthDelegationHandler(ITokenProviderService tokenProviderService)
        {
            _tokenProviderService = tokenProviderService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenProviderService.GetTokenAsync(TgBotHttpClientName);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
