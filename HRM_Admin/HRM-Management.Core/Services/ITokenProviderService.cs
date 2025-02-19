namespace HRM_Management.Core.Services
{
    public interface ITokenProviderService
    {
        Task<string> GetTokenAsync(string httpClientName);
    }
}
