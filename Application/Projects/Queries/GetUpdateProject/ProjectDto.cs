
using Application.Common;

namespace Application.Projects.Queries.GetProjectById
{
    public class ProjectDto : BaseDto
    {
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }
        public string? AdditionalInfo { get; set; }
        public bool IsActive { get; set; }
        public int? IdInstitution { get; set; }
        public int IdUser { get; set; }
    }
}
