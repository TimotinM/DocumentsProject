using Application.Common;

namespace Application.Istitutions.Commands.UpdateInstitution
{
    public class UpdateInstitutionDto : BaseDto
    {
        public string InstCode { get; set; }
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
