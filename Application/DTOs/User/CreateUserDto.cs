
using Application.DTOs.Institution;

namespace Application.DTOs.User
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public List<UserRoleDto> UserRole { get; set; }
        public InstitutionDto Institution { get; set; }
        public bool IsEnabled { get; set; }
    }
}
