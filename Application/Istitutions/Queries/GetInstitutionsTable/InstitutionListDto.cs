using Application.Common;

namespace Application.Istitutions.Queries.GetInstitutionsTable
{
    public class InstitutionListDto : BaseDto
    {
        public string InstCode { get; set; }
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
