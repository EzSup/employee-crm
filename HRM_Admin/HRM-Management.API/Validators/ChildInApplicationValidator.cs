using FluentValidation;
using HRM_Management.Core.DTOs.PersonDtos;
using HRM_Management.Core.Helpers;

namespace HRM_Management.API.Validators
{
    public class ChildInApplicationValidator : AbstractValidator<ChildInApplication>
    {
        public ChildInApplicationValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name must be specified");
            RuleFor(dto => dto.BirthDate).NotNull()
                .GreaterThanOrEqualTo(Constants.MIN_VALID_DATE).WithMessage("Birth date must be greater than 1900-01-01")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Birth date can`t be greater than current date!");
            RuleFor(dto => dto.Gender)
                .NotNull()
                .IsInEnum();
        }
    }
}
