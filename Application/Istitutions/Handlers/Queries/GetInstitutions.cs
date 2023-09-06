
using Application.Common.Interfaces;
using Application.DTOs.Institution;
using Application.Istitutions.Requests.Queris;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Istitutions.Handlers.Queries
{
    public class GetInstitutions : IRequestHandler<GetInstitutionsRequest, List<InstitutionDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetInstitutions(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<InstitutionDto>> Handle(GetInstitutionsRequest request, CancellationToken cancellationToken)
        {
            var institutionList = await _context.Institutions.Select(x => new InstitutionDto()
            {
                Id = x.Id,
                Name = x.Name,
                InstCode = x.InstCode,
                AdditionalInfo = x.AdditionalInfo
            }).ToListAsync(cancellationToken);

            return institutionList;
        }
    }
}
