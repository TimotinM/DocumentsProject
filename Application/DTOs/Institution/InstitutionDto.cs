
using Application.DTOs.Common;

namespace Application.DTOs.Institution
{
    public class InstitutionDto : BaseDto
    {
        public string InstCode { get; set; }
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
