using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Conmmand.UpdateUser
{
    public class UpdateUserCommand : IRequest<BaseCommandResponse>
    {
        public UpdateUserDto User { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BaseCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateUserDtoValidator(_userManager);
            var validationResult = await validator.ValidateAsync(request.User);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Update User Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var user = await _userManager.FindByIdAsync(request.User.Id.ToString());

                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRolesAsync(user, request.User.UserRoles);

                user.UserName = request.User.UserName;
                user.Email = request.User.Email;
                user.Name = request.User.Name;
                user.Surname = request.User.Surname;
                user.Patronymic = request.User.Patronymic;
                user.IdInstitution = request.User.IdInstitution;
                user.IsEnabled = request.User.IsEnabled;
                await _userManager.UpdateAsync(user);

                response.Success = true;
                response.Message = "User was updatet successful";
            }

            return response;
        }
    }
}
