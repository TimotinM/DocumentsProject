using Application.Common.Interfaces;
using Application.Responses.JsTree.DocumentsTree;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Queries.GetDocumentsTree
{
    public class GetDocumentsTreeRequest : IRequest<List<Institution>>
    {
    }

    public class GetDocumentsTreeRequestHandler : IRequestHandler<GetDocumentsTreeRequest, List<Institution>>
    {
        private readonly IApplicationDbContext _context;

        public GetDocumentsTreeRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Institution>> Handle(GetDocumentsTreeRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.Documents
                .Include(x => x.Institution)
                .Include(x => x.DocumentType).ThenInclude(x => x.Macro)
                .GroupBy(doc => doc.Institution)
                .Select(instGroup => new Institution()
                {
                    Text = instGroup.Key.Name,
                    Children = instGroup.
                        GroupBy(doc => doc.GroupingDate.Year)
                        .Select(yearGroup => new Year()
                        {
                            Text = yearGroup.Key.ToString(),
                            Children = yearGroup
                            .GroupBy(t =>t.DocumentType.IsMacro ? t.DocumentType.Name : t.DocumentType.Macro.FirstOrDefault().Name)
                                .Select(t => new MacroType()
                                {
                                    Text = t.Key.ToString(),                                   
                                }).ToList()
                        }).ToList()

                })
                .ToListAsync();

            return response;
        }
    }
}
