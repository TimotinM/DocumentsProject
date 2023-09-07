

using Application.Common.Interfaces;
using Application.DTOs.User;
using Application.Users.Requests.Queris;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Handlers.Queris
{
    public class GetUsers : IRequestHandler<GetUsersRequest, List<GetUsersListDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUsers(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<GetUsersListDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Select(x => new GetUsersListDto
            {
                UserName = x.UserName,
                Id = x.Id,
                Email = x.Email,
                NameSurname = x.Name + " " + x.Surname,
                IsEnabled = x.IsEnabled
            }).ToListAsync(cancellationToken);

            return users;
        }
    }
}
