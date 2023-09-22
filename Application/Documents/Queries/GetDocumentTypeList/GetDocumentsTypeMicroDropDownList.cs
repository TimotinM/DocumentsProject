
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Queries.GetDocumentTypeList
{
    public class GetDocumentsTypeMicroDropDownListRequest : IRequest<List<DocumentsTypeDropDownListDto>>
    {
        public int IdMacro { get; set; }
    }

    public class GetDocumentsTypeMicroDropDownListRequestHandler : IRequestHandler<GetDocumentsTypeMicroDropDownListRequest, List<DocumentsTypeDropDownListDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetDocumentsTypeMicroDropDownListRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<DocumentsTypeDropDownListDto>> Handle(GetDocumentsTypeMicroDropDownListRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.DocumentTypes
                .Include(x => x.Macro)
                .Where(x => x.Macro.FirstOrDefault().Id == request.IdMacro)
                .Select(x => new DocumentsTypeDropDownListDto()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            return response;
        }
    }
}
