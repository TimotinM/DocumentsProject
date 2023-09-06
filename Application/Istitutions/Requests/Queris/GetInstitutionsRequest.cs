
using Application.DTOs.Institution;
using MediatR;

namespace Application.Istitutions.Requests.Queris
{
    public class GetInstitutionsRequest : IRequest<List<InstitutionDto>>
    {
    }
}
