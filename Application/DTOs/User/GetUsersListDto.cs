
using Application.DTOs.Common;

namespace Application.DTOs.User
{
    public class GetUsersListDto : BaseDto
    {
        public bool IsEnabled { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
