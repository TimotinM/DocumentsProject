
using Application.Common.Interfaces;
using Application.Responses.JsTree;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Queries.GetProjectsTree
{
    public class GetProjectsTreeRequest : IRequest<List<JsTree>>
    {
    }

    public class GetProjectsTreeRequestHandler : IRequestHandler<GetProjectsTreeRequest, List<JsTree>>
    {
        private readonly IApplicationDbContext _context;

        public GetProjectsTreeRequestHandler(IApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<List<JsTree>> Handle(GetProjectsTreeRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.Projects
                .Include(x => x.Institution)
                .GroupBy(x => x.Institution)
                .Select(projectsGroup => new JsTree()
                {
                    Text = projectsGroup.Key.Name,
                    Column = 4,
                    Children = projectsGroup
                        .GroupBy(projectsGroup => projectsGroup.DateTill.Year)
                        .Select(yearGroup => new JsTree()
                        {
                            Text = yearGroup.Key.ToString(),
                            Column = 3
                        }).ToList()          
                }).ToListAsync();

            return response;
        }
    }
}
