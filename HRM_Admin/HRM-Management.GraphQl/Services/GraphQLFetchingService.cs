using HotChocolate.Execution;
using HRM_Management.Core.Services;
using Microsoft.AspNetCore.Http;

namespace HRM_Management.GraphQl.Services
{
    public class GraphQLFetchingService : IGraphQLFetchingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestExecutorResolver _resolver;

        public GraphQLFetchingService(IRequestExecutorResolver resolver, IHttpContextAccessor httpContextAccessor)
        {
            _resolver = resolver;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Execute(string query)
        {
            var executor = await _resolver.GetRequestExecutorAsync();
            var user = _httpContextAccessor.HttpContext.User;

            var userState = new UserState(user);

            var result = await executor.ExecuteAsync(request =>
            {
                request.SetDocument(query);
                request.TryAddGlobalState(WellKnownContextData.UserState, userState);
            });

            return result.ToJson();
        }
    }
}
