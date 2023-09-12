using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Conmmand
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateUserDtoValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email address is required")
                .EmailAddress()
                .WithMessage("A valid email is required")
                .MustAsync(async (email, cancellation) => await IsEmailUniqueAsync(email))
                .WithMessage("Email already taken");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password is required")
                .Equal(x => x.Password)
                .WithMessage("Password and Confirm Password must match");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required")
                .Matches(@"^[0-9a-zA-Z ]+$")
                .WithMessage("Only numbers and letters are alowed")
                .MaximumLength(32)
                .WithMessage("Username must be at most 32 characters long");
        }

        private async Task<bool> IsEmailUniqueAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null;
        }
    }
}
