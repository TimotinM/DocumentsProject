
using Application.DTOs.Common;

namespace Application.DTOs.User
{
    public class UsersListDto : BaseDto
    {
        public bool IsEnabled { get; set; }
        public string? NameSurname { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
