using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Queries.GetDocumentTypeList
{
    public class GetDocumentsTypeDropDownListRequest : IRequest<List<DocumentsTypeDropDownListDto>>
    {
        public bool IsMacro { get; set; }
    }

    public class GetDocumentsTypeDropDownListRequestHandler : IRequestHandler<GetDocumentsTypeDropDownListRequest, List<DocumentsTypeDropDownListDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetDocumentsTypeDropDownListRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<DocumentsTypeDropDownListDto>> Handle(GetDocumentsTypeDropDownListRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.DocumentTypes.Where(x => x.IsMacro ==  request.IsMacro)
                .Select(x => new DocumentsTypeDropDownListDto()
                {
                    Id = x.Id,
                    Name = x.Name,  
                }).ToListAsync();

            return result;
        }
    }
}
