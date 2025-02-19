using HRM_Management.Core.DTOs.Enums;

namespace HRM_Management.Core.DTOs.PersonDtos
{
    public record PartnerInForm(int Id, string? Name, Gender Gender, DateTime BirthDate);
}
