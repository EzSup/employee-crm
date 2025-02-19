using HRM_Management.Core.DTOs.Enums;

namespace HRM_Management.Core.DTOs.PartnerDtos
{
    public record PartnerUpdateRequest(int Id, string Name, DateTime BirthDate, Gender Gender, int PersonId);
}
