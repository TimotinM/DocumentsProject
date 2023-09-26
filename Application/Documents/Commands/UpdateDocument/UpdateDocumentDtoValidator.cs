
using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentDtoValidator : AbstractValidator<UpdateDocumentDto>
    {
        private readonly IApplicationDbContext _context;
        public UpdateDocumentDtoValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.AdditionalInfo)
                .MaximumLength(1000)
                .WithMessage("Additional information must not exceed 1000 characters!");

            RuleFor(x => x.IdMacro)
                .NotNull()
                .WithMessage("Macro type can't be empty");

            RuleFor(x => x.IdMicro)
                .NotEmpty()
                .WhenAsync(async (doc, cancellation) => doc.IdMacro != await GetSLATypeId())
                .WithMessage("Micro type is required");

            RuleFor(x => x.IdInstitution)
                .NotEmpty()
                .WithMessage("Institution is required");

            RuleFor(x => x.IdProject)
                .NotEmpty()
                .WhenAsync(async (doc, cancellation) => doc.IdMacro == await GetProjectTypeId())
                .WithMessage("Project is required");

        }

        private async Task<int> GetSLATypeId()
        {
            return await _context.DocumentTypes.Where(x => x.Name == "SLA").Select(x => x.Id).FirstOrDefaultAsync();
        }

        private async Task<int> GetProjectTypeId()
        {
            return await _context.DocumentTypes.Where(x => x.Name == "Project").Select(x => x.Id).FirstOrDefaultAsync();
        }
    }
}
