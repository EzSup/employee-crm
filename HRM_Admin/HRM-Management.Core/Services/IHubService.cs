using HRM_Management.Core.DTOs.HubDtos;

namespace HRM_Management.Core.Services
{
    public interface IHubService
    {
        Task<HubResponse> GetByIdWithJoinsAsync(int id);
        Task<int> AddAsync(HubCreateRequest request);
        Task UpdateAsync(HubUpdateRequest request);
        Task DeleteAsync(int id);
        Task AssignMembersAsync(int hubId, int[] employeesIds);
        Task DismissMembersAsync(int hubId, int[] employeesIds);
    }
}
