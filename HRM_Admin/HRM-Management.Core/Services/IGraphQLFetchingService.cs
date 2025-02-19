namespace HRM_Management.Core.Services
{
    public interface IGraphQLFetchingService
    {
        Task<string> Execute(string query);
    }
}
