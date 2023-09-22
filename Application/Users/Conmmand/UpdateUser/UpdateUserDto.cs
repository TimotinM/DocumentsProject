using Application.Common;

namespace Application.Users.Conmmand.UpdateUser
{
    public class UpdateUserDto : BaseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public List<string> UserRoles { get; set; } = new List<string>();
        public int? IdInstitution { get; set; }
        public bool IsEnabled { get; set; }
    }
}
