using FluentValidation;

namespace Application.Users.Conmmand.ChangeUserPassword
{
    public class ChangeUserPasswordDtoValidator : AbstractValidator<ChangeUserPasswordDto>
    {
        public ChangeUserPasswordDtoValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage("Current Password is required");

            RuleFor(x => x.NewPassword)
               .NotEmpty()
               .WithMessage("Password is required")
               .MinimumLength(6)
               .WithMessage("Password must be at least 6 characters long")
               .Matches(@"(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\-])^.{4,}")
               .WithMessage("Password must contain one upercase, lowercase, digit and special character");


            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .WithMessage("Confirm Password is required")
                .Equal(x => x.NewPassword)
                .WithMessage("Password and Confirm Password must match");
        }
    }
}
