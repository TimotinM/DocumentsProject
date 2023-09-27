
using Application.Common;

namespace Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectDto : BaseDto
    {
        public string Name { get; set; }
        public int? IdInstitution { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }
        public string? AdditionalInfo { get; set; }
        public bool IsActive { get; set; }
    }
}
