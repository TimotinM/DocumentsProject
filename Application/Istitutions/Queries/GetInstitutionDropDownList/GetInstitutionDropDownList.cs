
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Istitutions.Queries.GetInstitutionDropDownList
{
    public class GetInstitutionDropDownListRequest : IRequest<List<InstitutionDropDownListDto>>
    {
    }

    public class GetIstitutionDropDownListRequestHandler : IRequestHandler<GetInstitutionDropDownListRequest, List<InstitutionDropDownListDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetIstitutionDropDownListRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<InstitutionDropDownListDto>> Handle(GetInstitutionDropDownListRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.Institutions
                .Select(x => new InstitutionDropDownListDto()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            return response;
        }
    }
}
