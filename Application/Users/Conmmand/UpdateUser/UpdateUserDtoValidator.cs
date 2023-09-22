using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Conmmand.UpdateUser
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserDtoValidator(UserManager<ApplicationUser> userManager)
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
                .MustAsync(async (userDto, email, cancellation) => await IsEmailUniqueAsync(email, userDto.Id))
                .WithMessage("Email already taken");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required")
                .Matches(@"^[0-9a-zA-Z ]+$")
                .WithMessage("Only numbers and letters are alowed")
                .MustAsync(async (userDto, user, cancellation) => await IsUsernameUniqueAsync(user, userDto.Id))
                .WithMessage("Username already taken")
                .MaximumLength(32)
                .WithMessage("Username must be at most 32 characters long");

            RuleFor(x => x.UserRoles)
                .NotNull()
                .WithMessage("User must have rol");

            RuleFor(x => x.IdInstitution)
                .NotEmpty()
                .When(x => x.UserRoles.Contains("BankOperator"))
                .WithMessage("Institution is required");
        }

        private async Task<bool> IsEmailUniqueAsync(string email, int userId)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return (user != null && user.Id == userId) || user == null;
        }

        private async Task<bool> IsUsernameUniqueAsync(string username, int userId)
        {
            var user = await _userManager.FindByNameAsync(username);
            return (user != null && user.Id == userId) || user == null;
        }
    }
}
