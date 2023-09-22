using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queris.GetUserDetails
{
    public class GetUserDetailsRequest : IRequest<UserDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetUserDetailsRequestHandler : IRequestHandler<GetUserDetailsRequest, UserDetailsDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserDetailsRequestHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserDetailsDto> Handle(GetUserDetailsRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Include(x => x.Institution).FirstOrDefaultAsync(x => x.Id == request.Id);
            var roles = await _userManager.GetRolesAsync(user) as List<string>;
            var result = new UserDetailsDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                UserRoles = roles,
                Institution = user.Institution?.Name,
                IsEnabled = user.IsEnabled
            };

            return result;
        }
    }
}
