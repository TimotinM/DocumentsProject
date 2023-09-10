using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Institution
{
    public class CreateInstitutionDto
    {
        public string InstCode { get; set; }
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
