using FluentValidation;

namespace Application.Projects.Commands.CreateProject
{
    public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(x => x.IdInstitution)
                .NotEmpty()
                .WithMessage("Institution is required");

            RuleFor(x => x.DateFrom)
                .NotEmpty()
                .WithMessage("Start date is required");

            RuleFor(x => x.DateTill)
                .NotEmpty()
                .WithMessage("End date is required");

            RuleFor(x => x.AdditionalInfo)
                .MaximumLength(1000)
                .WithMessage("Information must not exceed 1000 characters");

        }
    }
}
