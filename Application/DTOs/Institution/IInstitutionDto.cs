
namespace Application.DTOs.Institution
{
    public interface IInstitutionDto
    {
        public string InstCode { get; set; }
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
