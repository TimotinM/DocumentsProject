using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queris.GetRolesDropDownList
{
    public class GetRolesDropDownListRequest : IRequest<List<RolesDropDownListDto>>
    {
    }

    public class GetRolesDropDownListRequestHandler : IRequestHandler<GetRolesDropDownListRequest, List<RolesDropDownListDto>>
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public GetRolesDropDownListRequestHandler(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<List<RolesDropDownListDto>> Handle(GetRolesDropDownListRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleManager.Roles
                .Select(x => new RolesDropDownListDto()
                {
                    Name = x.Name
                }).ToListAsync();

            return result;
        }
    }
}
