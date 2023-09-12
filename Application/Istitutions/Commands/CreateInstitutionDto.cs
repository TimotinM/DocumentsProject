using System.ComponentModel.DataAnnotations;

namespace Application.Istitutions.Commands
{
    public class CreateInstitutionDto
    {
        public string InstCode { get; set; }
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
