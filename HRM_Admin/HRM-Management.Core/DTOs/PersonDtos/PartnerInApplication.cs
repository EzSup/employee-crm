using HRM_Management.Core.DTOs.Enums;

namespace HRM_Management.Core.DTOs.PersonDtos
{
    public record PartnerInApplication(
        string Name,
        DateTime BirthDate,
        Gender Gender);
}
