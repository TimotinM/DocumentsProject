using Application.Common;
using Application.Common.Interfaces;
using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.TableParameters;

namespace Application.Istitutions.Queries.GetInstitutionsTable
{
    public class GetInstitutionsRequest : IRequest<DataTablesResponse<InstitutionListDto>>
    {
        public DataTablesParameters Parameters { get; set; }
    }

    public class GetInstitutions : IRequestHandler<GetInstitutionsRequest, DataTablesResponse<InstitutionListDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetInstitutions(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataTablesResponse<InstitutionListDto>> Handle(GetInstitutionsRequest request, CancellationToken cancellationToken)
        {
            var orderColumn = request.Parameters.Columns[request.Parameters.Order[0].Column].Name;
            var toFind = request.Parameters.Search.Value ?? "";

            var result = await _context.Institutions
                .Where(x => x.Name.Contains(toFind)
                    || x.InstCode.Contains(toFind)
                    || x.AdditionalInfo.Contains(toFind))
                .OrderByExtension(orderColumn, request.Parameters.Order[0].Dir)
                .Skip(request.Parameters.Start)
                .Take(request.Parameters.Length)
                .Select(x => new InstitutionListDto
                {
                    Name = x.Name,
                    InstCode = x.InstCode,
                    AdditionalInfo = x.AdditionalInfo
                })
                .ToListAsync();

            var total = await _context.Institutions
                .Where(x => x.Name.Contains(toFind)
                    || x.InstCode.Contains(toFind)
                    || x.AdditionalInfo.Contains(toFind))
                .CountAsync();

            var response = new DataTablesResponse<InstitutionListDto>()
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
