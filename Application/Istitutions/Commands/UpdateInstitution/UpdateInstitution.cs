using Application.Common.Interfaces;
using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Istitutions.Commands.UpdateInstitution
{
    public class UpdateInstitutionRequest : IRequest<BaseCommandResponse>
    {
        public UpdateInstitutionDto InstitutionDto { get; set; }
    }

    public class UpdateInstitutionRequestHandler : IRequestHandler<UpdateInstitutionRequest, BaseCommandResponse>
    {
        private readonly IApplicationDbContext _context;

        public UpdateInstitutionRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BaseCommandResponse> Handle(UpdateInstitutionRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateInstitutionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.InstitutionDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Edit Institution Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var institution = await _context.Institutions
                    .SingleAsync(x => x.Id == request.InstitutionDto.Id);

                institution.InstCode = request.InstitutionDto.InstCode;
                institution.Name = request.InstitutionDto.Name;
                institution.AdditionalInfo = request.InstitutionDto.AdditionalInfo;

                await _context.SaveChangesAsync(cancellationToken);

                response.Success = true;
                response.Message = "Institution was edited successful";

            }

            return response;
        }
    }
}
