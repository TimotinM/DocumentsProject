
using Application.Common.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Conmmand.UpdateUser
{
    public class UpdateUserEnabledCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserEnabledCommand, BaseCommandResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BaseCommandResponse> Handle(UpdateUserEnabledCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            user.IsEnabled = !user.IsEnabled;
            await _userManager.UpdateAsync(user);

            return response;
        }
    }
}
