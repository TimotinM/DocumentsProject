
using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Commands.CreateDocument
{
    public class CreateDocumentDtoValidator : AbstractValidator<CreateDocumentDto>
    {
        private readonly IApplicationDbContext _context;
        public CreateDocumentDtoValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.SavePath)
                .NotEmpty()
                .WithMessage("File is required");

            RuleFor(x => x.AdditionalInfo)
                .MaximumLength(1000)
                .WithMessage("Additional information must not exceed 1000 characters!");

            RuleFor(x => x.IdMacro)
                .NotNull()
                .WithMessage("Macro type can't be empty");

            RuleFor(x => x.IdMicro)
                .NotEmpty()
                .When(x => x.IdMacro.Equals(GetSLAId()))
                .WithMessage("Macro type is required");

            RuleFor(x => x.IdInstitution)
                .NotEmpty()
                .WithMessage("Institution is required");

            RuleFor(x => x.IdProject)
                .NotEmpty()
                .When(x => x.IdMacro.Equals(GetDesignId()))
                .WithMessage("Design is required");

        }

        private async Task<int> GetSLAId()
        {
            return await _context.DocumentTypes.Where(x => x.Name == "SLA").Select(x => x.Id).FirstOrDefaultAsync();
        }

        private async Task<int> GetDesignId()
        {
            return await _context.DocumentTypes.Where(x => x.Name == "Design").Select(x => x.Id).FirstOrDefaultAsync();
        }
    }
}
