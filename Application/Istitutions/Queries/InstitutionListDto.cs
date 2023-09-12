using Application.Common;

namespace Application.Istitutions.Queries
{
    public class InstitutionListDto : BaseDto
    {
        public string InstCode { get; set; }
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
