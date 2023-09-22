using Application.Common.Interfaces;
using Application.Responses.JsTree;
using Application.Responses.JsTree.DocumentsTree;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Queries.GetDocumentsTree
{
    public class GetDocumentsTreeRequest : IRequest<List<JsTree>>
    {
    }

    public class GetDocumentsTreeRequestHandler : IRequestHandler<GetDocumentsTreeRequest, List<JsTree>>
    {
        private readonly IApplicationDbContext _context;

        public GetDocumentsTreeRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<JsTree>> Handle(GetDocumentsTreeRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.Documents
                .Include(x => x.Institution)
                .Include(x => x.DocumentType).ThenInclude(x => x.Macro)
                .GroupBy(doc => doc.Institution)
                .Select(instGroup => new JsTree()
                {
                    Column = 4,
                    Text = instGroup.Key.Name,
                    Children = instGroup.
                        GroupBy(doc => doc.GroupingDate.Year)
                        .Select(yearGroup => new JsTree()
                        {
                            Column = 3,
                            Text = yearGroup.Key.ToString(),
                            Children = yearGroup
                            .GroupBy(t =>t.DocumentType.IsMacro ? t.DocumentType.Name : t.DocumentType.Macro.FirstOrDefault().Name)
                                .Select(t => new JsTree()
                                {
                                    Column = 2,
                                    Text = t.Key.ToString(),                                   
                                }).ToList()
                        }).ToList()

                })
                .ToListAsync();

            return response;
        }
    }
}
