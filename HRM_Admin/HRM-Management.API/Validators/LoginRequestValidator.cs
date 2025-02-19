using FluentValidation;
using HRM_Management.Core.DTOs.AuthDtos;
using HRM_Management.Core.Helpers;

namespace HRM_Management.API.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(dto => dto.Username)
                .NotEmpty().WithMessage("Username must be provided!")
                .EmailAddress()
                .Matches(Constants.CORPORATIVE_EMAIL_REGEX_PATTERN).WithMessage("Username(Email) must be with the domain '@ sysdev.com'");
            RuleFor(dto => dto.Password).NotEmpty();
        }
    }
}
