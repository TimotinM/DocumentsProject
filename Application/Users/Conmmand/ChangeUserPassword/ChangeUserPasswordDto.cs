using Application.Common;

namespace Application.Users.Conmmand.ChangeUserPassword
{
    public class ChangeUserPasswordDto : BaseDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
