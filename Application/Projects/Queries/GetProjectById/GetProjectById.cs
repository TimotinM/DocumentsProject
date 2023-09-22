using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdRequest : IRequest<ProjectDto>
    {
        public int Id { get; set; }
    }

    public class GetProjectByIdRequestHandler : IRequestHandler<GetProjectByIdRequest, ProjectDto>
    {
        private readonly IApplicationDbContext _context;

        public GetProjectByIdRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Projects
                    .Where(x => x.Id == request.Id)
                    .Select(x => new ProjectDto()
                    {
                        Id = x.Id,
                        DateFrom = x.DateFrom,
                        DateTill = x.DateTill,
                        Name = x.Name,
                        IdInstitution = x.IdInstitution,
                        AdditionalInfo = x.AdditionalInfo,
                        IdUser = x.IdUser,
                        IsActive = x.IsActive
                    }).FirstAsync();

            return result;
        }
    }
}
