
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Queries.GetDocumentById
{
    public class GetDocumentByIdRequest : IRequest<DocumentDto>
    {
        public int Id { get; set; }
    }

    public class GetDocumentByIdRequestHandler : IRequestHandler<GetDocumentByIdRequest, DocumentDto>
    {
        private readonly IApplicationDbContext _context;

        public GetDocumentByIdRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DocumentDto> Handle(GetDocumentByIdRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.Documents
                .Include(x => x.DocumentType)
                    .ThenInclude(x => x.Macro)
                .Select(x => new DocumentDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    GroupingDate = x.GroupingDate,
                    IdInstitution = x.IdInstitution,
                    IdMacro = x.DocumentType.IsMacro
                        ? x.IdType
                        : x.DocumentType.Macro.First().Id,
                    IdMicro = x.DocumentType.IsMacro
                        ? null
                        : x.IdType,
                    IdProject = x.IdProject,
                    AdditionalInfo = x.AdditionalInfo
                }).FirstAsync(x => x.Id == request.Id);

            return response;
        }
    }
}
