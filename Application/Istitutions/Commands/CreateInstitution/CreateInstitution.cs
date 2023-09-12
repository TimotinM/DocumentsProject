using Application.Common.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Istitutions.Commands.CreateInstitution
{
    public class CreateInstitutionCommand : IRequest<BaseCommandResponse>
    {
        public CreateInstitutionDto InstitutionDto { get; set; }
    }
    public class CreateInstitutionCommandHandler : IRequestHandler<CreateInstitutionCommand, BaseCommandResponse>
    {
        private readonly IApplicationDbContext _context;

        public CreateInstitutionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BaseCommandResponse> Handle(CreateInstitutionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateInstitutionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.InstitutionDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Create Institution Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var entity = new Institution
                {
                    InstCode = request.InstitutionDto.InstCode,
                    Name = request.InstitutionDto.Name,
                    AdditionalInfo = request.InstitutionDto.AdditionalInfo,
                    Created = DateTime.Now
                };
                _context.Institutions.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);
                response.Success = true;
                response.Message = "Institution was created successful";
            }

            return response;
        }
    }
}
