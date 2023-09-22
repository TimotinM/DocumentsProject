using Application.Users.Conmmand.UpdateUser;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queris
{
    public class GetUserByIdRequest : IRequest<UpdateUserDto>
    {
        public int Id { get; set; }
    }

    public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, UpdateUserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserByIdRequestHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UpdateUserDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            var roles = await _userManager.GetRolesAsync(user) as List<string>;
            var result = new UpdateUserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                UserRoles = roles,
                IdInstitution = user.IdInstitution,
                IsEnabled = user.IsEnabled
            };

            return result;
        }
    }
}
