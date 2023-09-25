
using Application.Common;

namespace Application.Istitutions.Queries.GetInstitutionById
{
    public class InstitutionDto : BaseDto
    {
        public string InstCode { get; set; }
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
