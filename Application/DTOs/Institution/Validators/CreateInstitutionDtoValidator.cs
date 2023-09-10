using FluentValidation;

namespace Application.DTOs.Institution.Validators
{
    public class CreateInstitutionDtoValidator : AbstractValidator<CreateInstitutionDto>
    {
        public CreateInstitutionDtoValidator()
        {
            RuleFor(n => n.Name)
               .NotNull()
               .WithMessage("Name is required.")
               .MaximumLength(255)
               .WithMessage("Name must not exceed 255 characters.");

            RuleFor(c => c.InstCode)
                .NotNull()
                .WithMessage("Code is required.")
                .MaximumLength(5)
                .WithMessage("Code must not exceed 5 characters.");

            RuleFor(i => i.AdditionalInfo)
                .MaximumLength(1000)
                .WithMessage("Information must not exceed 1000 characters.");
        }
    }
}
