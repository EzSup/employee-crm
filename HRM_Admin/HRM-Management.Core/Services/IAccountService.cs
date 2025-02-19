using HRM_Management.Core.DTOs.AuthDtos;

namespace HRM_Management.Core.Interfaces
{
    public interface IAccountService
    {
        Task Register(RegisterRequest request);
        Task Login(LoginRequest request);
        Task Logout();
        Task<UserResponse> GetAccountInfoAsync(int id);
    }
}
