using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Queries.GetProjectDetails
{
    public class GetProjectDetailsRequest : IRequest<ProjectDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetProjectDetailsRequestHandler : IRequestHandler<GetProjectDetailsRequest, ProjectDetailsDto>
    {
        private readonly IApplicationDbContext _context;

        public GetProjectDetailsRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectDetailsDto> Handle(GetProjectDetailsRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Projects
                .Include(x => x.Institution)
                .Include(x => x.ApplicationUser)
                .Where(x => x.Id == request.Id)
                .Select(x => new ProjectDetailsDto()
                {
                    Name = x.Name,
                    Institution = x.Institution.Name,
                    DateFrom = x.DateFrom,
                    DateTill = x.DateTill,
                    AdditionalInfo = x.AdditionalInfo,
                    IsActive = x.IsActive,
                    User = $"{x.ApplicationUser.Name} {x.ApplicationUser.Surname}"
                }).FirstAsync();

            return result;
        }
    }
}
