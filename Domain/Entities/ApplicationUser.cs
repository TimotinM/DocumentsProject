using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }

        public int? IdInstitution { get; set; }
        public Institution? Institution { get; set; }

        public IList<Document>? Documents { get; set; }
        public IList<Project>? Projects { get; set; }
    }
}
