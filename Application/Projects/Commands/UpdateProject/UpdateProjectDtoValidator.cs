
using FluentValidation;

namespace Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectDtoValidator : AbstractValidator<UpdateProjectDto>
    {
        public UpdateProjectDtoValidator()
        {
            RuleFor(x => x.Name)
                 .NotEmpty()
                 .WithMessage("Name is required");

            RuleFor(x => x.IdInstitution)
                .NotEmpty()
                .WithMessage("Institution is required");

            RuleFor(x => x.DateTill)
                .NotEmpty()
                .WithMessage("End date is required")
                .Must((dto, dateTill) => dateTill > dto.DateFrom)
                .WithMessage("End date must be greater than Start date"); ;

            RuleFor(x => x.AdditionalInfo)
                .MaximumLength(1000)
                .WithMessage("Information must not exceed 1000 characters");
        }
    }
}
