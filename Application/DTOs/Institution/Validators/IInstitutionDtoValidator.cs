using FluentValidation;

namespace Application.DTOs.Institution.Validators
{
    public class IInstitutionDtoValidator : AbstractValidator<IInstitutionDto>
    {
        public IInstitutionDtoValidator()
        { 
            RuleFor(n => n.Name)
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(255)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(c => c.InstCode)
                .NotNull()
                .WithMessage("Code is required.")
                .MaximumLength(5)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(i => i.AdditionalInfo)
                .MaximumLength(1000)
                .WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
