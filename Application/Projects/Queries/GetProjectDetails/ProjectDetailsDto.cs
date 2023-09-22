
namespace Application.Projects.Queries.GetProjectDetails
{
    public class ProjectDetailsDto
    {
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }
        public string? AdditionalInfo { get; set; }
        public bool IsActive { get; set; }
        public string Institution { get; set; }
        public string User { get; set; }
    }
}
