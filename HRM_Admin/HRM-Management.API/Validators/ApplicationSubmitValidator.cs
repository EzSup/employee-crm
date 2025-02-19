using FluentValidation;
using HRM_Management.Core.DTOs.PersonDtos;
using HRM_Management.Core.Helpers;

namespace HRM_Management.API.Validators
{
    public class ApplicationSubmitValidator : AbstractValidator<ApplicationSubmitRequest>
    {
        public ApplicationSubmitValidator()
        {
            RuleFor(dto => dto.TelegramId).NotEmpty();
            RuleFor(dto => dto.FullNameEn).Matches(Constants.FULL_NAME_EN_REGEX_PATTERN)
                .WithMessage("Invalid Full Name in English");
            RuleFor(dto => dto.FullNameUa).Matches(Constants.FULL_NAME_UA_REGEX_PATTERN)
                .WithMessage("Invalid Full Name in Ukrainian");
            RuleFor(dto => dto.Email).EmailAddress();
            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.")
                .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(15).WithMessage("PhoneNumber must not exceed 15 characters.")
                .Matches(Constants.PHONE_NUM_REGEX_PATTERN).WithMessage("PhoneNumber must contain only numbers and optionally one '+'");
            RuleFor(dto => dto.BirthDate).NotNull()
                .GreaterThanOrEqualTo(Constants.MIN_VALID_DATE).WithMessage("Birth date must be greater than 1900-01-01")
                .LessThanOrEqualTo(DateTime.UtcNow.AddYears(-16)).WithMessage("Birth date must be valid for age not less than 16 y.o.");
            RuleFor(x => x.Hobbies)
                .MinimumLength(5).WithMessage("Field must be logner than 4 characters!");
            RuleFor(x => x.TechStack)
                .MinimumLength(5).WithMessage("Field must be logner than 4 characters!");
            RuleFor(x => x.EnglishLevel).IsInEnum();
            RuleFor(x => x.Gender).IsInEnum();
            RuleFor(x => x.TShirtSize).IsInEnum();
            RuleForEach(x => x.Children)
                .SetValidator(new ChildInApplicationValidator());
            RuleFor(x => x.Partner)
                .SetValidator(new PartnerInApplicationRequestValidator()!);
        }
    }
}
