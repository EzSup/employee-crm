using HRM_Management.Core.DTOs.PartnerDtos;

namespace HRM_Management.Core.Services
{
    public interface IPartnerService
    {
        Task<int> CreateAsync(PartnerCreateRequest request);
        Task UpdateAsync(PartnerUpdateRequest request);
        Task DeleteAsync(int id);
    }
}
