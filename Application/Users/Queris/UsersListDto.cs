using Application.Common;

namespace Application.Users.Queris
{
    public class UsersListDto : BaseDto
    {
        public bool IsEnabled { get; set; }
        public string? NameSurname { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
