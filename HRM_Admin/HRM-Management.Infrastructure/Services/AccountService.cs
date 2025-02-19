using AutoMapper;
using HRM_Management.Core.DTOs.AuthDtos;
using HRM_Management.Core.Helpers;
using HRM_Management.Core.Interfaces;
using HRM_Management.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using HRM_Management.Core.Helpers.Exceptions;

namespace HRM_Management.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;

        public AccountService(UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task Register(RegisterRequest request)
        {
            if (await UserExists(request.Email))
                throw new IdentifierAlreadyTakenException();
            var user = _mapper.Map<UserEntity>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new Exception($"{result.Errors}");
            await Login(_mapper.Map<LoginRequest>(request));
        }

        public async Task Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user is not null && user.UserName is not null)
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!isPasswordValid)
                    throw new IncorrectPasswordException();
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email)
                };
                await _signInManager.SignInWithClaimsAsync(user, true, claims);
            }
            else
                throw new IncorrectPasswordException(Constants.USERNAME_OR_PASSWORD_NOT_PROVIDED_MESSAGE);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserResponse> GetAccountInfoAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString())
                       ?? throw new UserNotFoundException();
            var userDto = _mapper.Map<UserResponse>(user);
            return userDto;

        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.FindByNameAsync(username) != null;
        }
    }
}
