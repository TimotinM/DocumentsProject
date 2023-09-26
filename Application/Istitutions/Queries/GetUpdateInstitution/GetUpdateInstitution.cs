using Application.Common.Interfaces;
using Application.Istitutions.Commands.UpdateInstitution;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Istitutions.Queries.GetUpdateInstitution
{
    public class GetUpdateInstitutionRequest : IRequest<UpdateInstitutionDto>
    {
        public int Id { get; set; }
    }

    public class GetUpdateInstitutionRequestHandler : IRequestHandler<GetUpdateInstitutionRequest, UpdateInstitutionDto>
    {
        private readonly IApplicationDbContext _context;
        
        public GetUpdateInstitutionRequestHandler(IApplicationDbContext context)
        {
        
            _context = context;
        }
        public async Task<UpdateInstitutionDto> Handle(GetUpdateInstitutionRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.Institutions
                .Select(x => new UpdateInstitutionDto()
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
