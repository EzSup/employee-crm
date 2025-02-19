namespace HRM_Management.Core.DTOs.AuthDtos
{
    public record UserResponse(
        int Id,
        string Username,
        string FullName
        );
}
