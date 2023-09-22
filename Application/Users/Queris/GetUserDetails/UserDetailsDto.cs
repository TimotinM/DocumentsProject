
namespace Application.Users.Queris.GetUserDetails
{
    public class UserDetailsDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public List<string> UserRoles { get; set; } = new List<string>();
        public string? Institution { get; set; }
        public bool IsEnabled { get; set; }
    }
}
