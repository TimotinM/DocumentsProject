
using Application.DTOs.User.Validators;
using Application.Responses;
using Application.Users.Requests.Conmmand;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Handlers.Conmmand
{
    public class CreateUserComandHandler : IRequestHandler<CreateUserCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public CreateUserComandHandler(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore)
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
                var result = await _userManager.CreateAsync(user, request.UserDto.Password);

                response.Success = true;
                response.Message = "Institution was created successful";
            }
            return response;
        }
    }
}
