using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Istitutions.Queries.GetInstitutionById
{
    public class GetInstitutionByIdRequest : IRequest<InstitutionDto>
    {
        public int Id { get; set; }
    }

    public class GetInstitutionByIdRequestHandler : IRequestHandler<GetInstitutionByIdRequest, InstitutionDto>
    {
        private readonly IApplicationDbContext _context;
        
        public GetInstitutionByIdRequestHandler(IApplicationDbContext context)
        {
        
            _context = context;
        }
        public async Task<InstitutionDto> Handle(GetInstitutionByIdRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.Institutions
                .Select(x => new InstitutionDto()
                {
                    Id = x.Id,
                    InstCode = x.InstCode,
                    Name = x.Name,
                    AdditionalInfo = x.AdditionalInfo
                }).FirstAsync(x => x.Id == request.Id);
            
            return response;
        }
    }
}
