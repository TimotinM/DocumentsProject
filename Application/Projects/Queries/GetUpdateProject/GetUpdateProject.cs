using Application.Common.Interfaces;
using Application.Projects.Commands.UpdateProject;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Queries.GetUpdateProject
{
    public class GetUpdateProjectRequest : IRequest<UpdateProjectDto>
    {
        public int Id { get; set; }
    }

    public class GetUpdateProjectRequestHandler : IRequestHandler<GetUpdateProjectRequest, UpdateProjectDto>
    {
        private readonly IApplicationDbContext _context;

        public GetUpdateProjectRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateProjectDto> Handle(GetUpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Projects
                    .Where(x => x.Id == request.Id)
                    .Select(x => new UpdateProjectDto()
                    {
                        Id = x.Id,
                        DateFrom = x.DateFrom,
                        DateTill = x.DateTill,
                        Name = x.Name,
                        IdInstitution = x.IdInstitution,
                        AdditionalInfo = x.AdditionalInfo,
                        IsActive = x.IsActive
                    }).FirstAsync();

            return result;
        }
    }
}
