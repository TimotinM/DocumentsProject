using Application.DTOs.Institution;
using Application.Responses;
using MediatR;

namespace Application.Istitutions.Requests.Commands
{
    public class CreateInstitutionCommand : IRequest<BaseCommandResponse>
    {
        public CreateInstitutionDto InstitutionDto { get; set; }
    }
}
