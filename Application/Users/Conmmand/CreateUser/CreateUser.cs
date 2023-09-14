#nullable disable
using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Users.Conmmand.CreateUser
{
    public class CreateUserCommand : IRequest<BaseCommandResponse>
    {
        public CreateUserDto UserDto { get; set; }
    }

    public class CreateUser : IRequestHandler<CreateUserCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public CreateUser(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore)
        {
            _userManager = userManager;
            _userStore = userStore;
        }

        public async Task<BaseCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateUserDtoValidator(_userManager);
            var validationResult = await validator.ValidateAsync(request.UserDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Create User Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var user = new ApplicationUser();

                await _userStore.SetUserNameAsync(user, request.UserDto.UserName, CancellationToken.None);
                user.Email = request.UserDto.Email;
                user.IsEnabled = request.UserDto.IsEnabled;
                user.Name = request.UserDto.Name;
                user.Surname = request.UserDto.Surname;
                user.Patronymic = request.UserDto.Patronymic;
                user.IdInstitution = request.UserDto.IdInstitution;
                var result = await _userManager.CreateAsync(user, request.UserDto.Password);
                await _userManager.AddToRolesAsync(user, request.UserDto.UserRoles);
                await _userManager.AddClaimAsync(user, new Claim("UserFullName", user.Name + " " + user.Surname));

                response.Success = true;
                response.Message = "Institution was created successful";
            }
            return response;
        }
    }
}
