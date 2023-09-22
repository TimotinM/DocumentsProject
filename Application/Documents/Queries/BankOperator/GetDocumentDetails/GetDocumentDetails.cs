using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Queries.BankOperator.GetDocumentDetails
{
    public class GetDocumentDetailsRequest : IRequest<DocumentsDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetDocumentDetailsRequestHandler : IRequestHandler<GetDocumentDetailsRequest, DocumentsDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        public GetDocumentDetailsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DocumentsDetailsDto> Handle(GetDocumentDetailsRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.Documents
                .Include(x => x.DocumentType).ThenInclude(x => x.Macro)
                .Include(x => x.Institution)
                .Include(x => x.ApplicationUser)
                .Include(x => x.Project)
                .Where(x => x.Id == request.Id)
                .Select(x => new DocumentsDetailsDto()
                {
                    Name = x.Name,
                    Institution = x.Institution.Name,
                    Macro = x.DocumentType.IsMacro? x.DocumentType.Name : x.DocumentType.Macro.FirstOrDefault().Name,
                    Micro = x.DocumentType.IsMacro? "" : x.DocumentType.Name,
                    Project = x.Project.Name,
                    GroupingDate = x.GroupingDate,
                    AdditionalInfo = x.AdditionalInfo,
                    User = $"{x.ApplicationUser.Name} {x.ApplicationUser.Surname}"
                }).FirstOrDefaultAsync(cancellationToken);

            return response;
        }
    }
}
