using FluentValidation;

namespace Application.DTOs.Institution.Validators
{
    public class CreateInstitutionDtoValidator : AbstractValidator<CreateInstitutionDto>
    {
        public CreateInstitutionDtoValidator()
        {
            Include(new IInstitutionDtoValidator());
        }
    }
}
