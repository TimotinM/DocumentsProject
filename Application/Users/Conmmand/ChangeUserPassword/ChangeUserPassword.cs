using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Conmmand.ChangeUserPassword
{
    public class ChangeUserPasswordCommand : IRequest<BaseCommandResponse>
    {
        public ChangeUserPasswordDto UserPassword { get; set; }
    }
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangeUserPasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BaseCommandResponse> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new ChangeUserPasswordDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UserPassword);

            if(!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Change User Password Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {
               var user = await _userManager.FindByIdAsync(request.UserPassword.Id.ToString());
               var result = await _userManager.ChangePasswordAsync(user, request.UserPassword.CurrentPassword.ToString(), request.UserPassword.NewPassword.ToString());
               if(!result.Succeeded)
                {
                    response.Success = false;
                    response.Message = "Change User Password Failed";
                    response.Errors = result.Errors.Select(e => e.Description).ToList();
                }
                else
                {
                    response.Success = true;
                    response.Message = "New password was created successful";
                }
            }
            
            return response;
        }
    }
}
