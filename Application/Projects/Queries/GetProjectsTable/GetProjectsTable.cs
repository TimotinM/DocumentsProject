using Application.Common;
using Application.Common.Interfaces;
using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.TableParameters;

namespace Application.Projects.Queries.GetProjectsTable
{
    public class GetProjectsTableRequest : IRequest<DataTablesResponse<ProjectTableDto>>
    {
        public DataTablesParameters Parameters { get; set; }
    }

    public class GetProjectsTableRequestHandler : IRequestHandler<GetProjectsTableRequest, DataTablesResponse<ProjectTableDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetProjectsTableRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataTablesResponse<ProjectTableDto>> Handle(GetProjectsTableRequest request, CancellationToken cancellationToken)
        {
            var orderColumn = request.Parameters.Columns[request.Parameters.Order[0].Column].Name;
            var toFind = request.Parameters.Search.Value ?? "";

            var query = _context.Projects
                 .Include(x => x.Institution)
                 .Include(x => x.ApplicationUser)
                 .Where(x => x.Name.Contains(toFind)
                     || x.Institution.Name.Contains(toFind)
                     || x.ApplicationUser.UserName.Contains(toFind)
                     || x.DateFrom.Year.ToString().Contains(toFind)
                     || x.DateTill.Year.ToString().Contains(toFind));

            var total = await query.CountAsync();

            var result = await query
                .OrderByExtension(orderColumn, request.Parameters.Order[0].Dir)
                .Skip(request.Parameters.Start)
                .Take(request.Parameters.Length)
                .Select(x => new ProjectTableDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    DateFrom = x.DateFrom,
                    DateTill = x.DateTill,
                    Institution = x.Institution.Name,
                    UserName = x.ApplicationUser.UserName,
                    IsActive = x.IsActive
                }).ToListAsync();

            var response = new DataTablesResponse<ProjectTableDto>()
            {
                Draw = request.Parameters.Draw,
                RecordsFiltered = total,
                RecordsTotal = total,
                Data = result
            };

            return response;
        }
    }
}
