using HRM_Management.Core.DTOs.Enums;

namespace HRM_Management.Core.DTOs.PersonDtos
{
    public record ChildInForm(int Id, string? Name, Gender Gender, DateTime BirthDate);
}
