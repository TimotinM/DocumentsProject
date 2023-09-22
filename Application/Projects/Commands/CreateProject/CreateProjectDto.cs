namespace Application.Projects.Commands.CreateProject
{
    public class CreateProjectDto
    {
        public string Name { get; set; }
        public int? IdInstitution { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }
        public string? AdditionalInfo { get; set; }
        public int IdUser { get; set; }

    }
}
