using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int IdInstitution { get; set; }
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}
