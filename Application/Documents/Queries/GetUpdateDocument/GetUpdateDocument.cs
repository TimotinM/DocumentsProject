using Application.Common.Interfaces;
using Application.Documents.Commands.UpdateDocument;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Queries.GetDocumentById
{
    public class GetUpdateDocument : IRequest<UpdateDocumentDto>
    {
        public int Id { get; set; }
    }

    public class GetUpdateDocumentHandler : IRequestHandler<GetUpdateDocument, UpdateDocumentDto>
    {
        private readonly IApplicationDbContext _context;

        public GetUpdateDocumentHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UpdateDocumentDto> Handle(GetUpdateDocument request, CancellationToken cancellationToken)
        {
            var response = await _context.Documents
                .Include(x => x.DocumentType)
                    .ThenInclude(x => x.Macro)
                .Select(x => new UpdateDocumentDto()
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
