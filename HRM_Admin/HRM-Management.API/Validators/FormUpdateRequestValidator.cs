using FluentValidation;
using HRM_Management.Core.DTOs.PersonDtos;
using HRM_Management.Core.Helpers;

namespace HRM_Management.API.Validators
{
    public class FormUpdateRequestValidator : AbstractValidator<FormUpdateRequest>
    {
        public FormUpdateRequestValidator()
        {
            RuleFor(dto => dto.TelegramId).NotEmpty();
            RuleFor(dto => dto.FNameEn).Matches(Constants.EN_NAME_REGEX_PATTERN)
                .WithMessage("Invalid First Name in English");
            RuleFor(dto => dto.MNameEn).Matches(Constants.EN_NAME_REGEX_PATTERN)
                .WithMessage("Invalid Middle Name in English");
            RuleFor(dto => dto.LNameEn).Matches(Constants.EN_NAME_REGEX_PATTERN)
                .WithMessage("Invalid Last Name in English");
            RuleFor(dto => dto.FNameUk).Matches(Constants.UA_NAME_REGEX_PATTERN)
                .WithMessage("Invalid First Name in Ukrainian");
            RuleFor(dto => dto.MNameUk).Matches(Constants.UA_NAME_REGEX_PATTERN)
                .WithMessage("Invalid Middle Name in Ukrainian");
            RuleFor(dto => dto.LNameUk).Matches(Constants.UA_NAME_REGEX_PATTERN)
                .WithMessage("Invalid Last Name in Ukrainian");
            RuleFor(dto => dto.PersonalEmail).EmailAddress();
            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.")
                .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(15).WithMessage("PhoneNumber must not exceed 15 characters.")
                .Matches(Constants.PHONE_NUM_REGEX_PATTERN).WithMessage("PhoneNumber must contain only numbers and optionally one '+'");
            RuleFor(dto => dto.BirthDate).NotNull()
                .GreaterThanOrEqualTo(Constants.MIN_VALID_DATE).WithMessage("Birth date must be greater than 1900-01-01")
                .LessThanOrEqualTo(DateTime.UtcNow.AddYears(-18)).WithMessage("Birth date must be valid for age not less than 18 y.o.");
            RuleFor(x => x.Hobbies)
                .MinimumLength(5).WithMessage("Field must be logner than 4 characters!");
            RuleFor(x => x.TechStack)
                .MinimumLength(5).WithMessage("Field must be logner than 4 characters!");
            RuleFor(x => x.EnglishLevel).IsInEnum();
            RuleFor(x => x.Gender).IsInEnum();
            RuleFor(x => x.TShirtSize).IsInEnum();
        }
    }
}
