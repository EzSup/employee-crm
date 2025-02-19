using FluentValidation;
using HRM_Management.Core.DTOs.AuthDtos;
using HRM_Management.Core.Helpers;

namespace HRM_Management.API.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage("Email must be provided!")
                .EmailAddress()
                .Matches(Constants.CORPORATIVE_EMAIL_REGEX_PATTERN).WithMessage("Username(Email) must be with the domain '@ sysdev.com'");
            RuleFor(dto => dto.FullName).Matches(Constants.FULL_NAME_UA_REGEX_PATTERN)
                .WithMessage("Invalid Full Name in Ukrainian");
            RuleFor(dto => dto.Password).NotEmpty();
        }
    }
}
