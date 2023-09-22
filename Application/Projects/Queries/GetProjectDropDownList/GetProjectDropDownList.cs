using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Queries.GetProjectDropDownList
{
    public class GetProjectDropDownListRequest : IRequest<List<ProjectsDropDownListDto>>
    {
    }

    public class GetProjectDropDownListRequestHandler : IRequestHandler<GetProjectDropDownListRequest, List<ProjectsDropDownListDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetProjectDropDownListRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProjectsDropDownListDto>> Handle(GetProjectDropDownListRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.Projects
                .Select(x => new ProjectsDropDownListDto()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
            
            return response;  
        }
    }
}
