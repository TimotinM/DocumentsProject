using Application.Common;
using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.TableParameters;

namespace Application.Users.Queris
{
    public class GetUsersRequest : IRequest<DataTablesResponse<UsersListDto>>
    {
        public DataTablesParameters Parameters { get; set; }
    }

    public class GetUsers : IRequestHandler<GetUsersRequest, DataTablesResponse<UsersListDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUsers(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<DataTablesResponse<UsersListDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var orderColumn = request.Parameters.Columns[request.Parameters.Order[0].Column].Name == "nameSurname" ? "name" : request.Parameters.Columns[request.Parameters.Order[0].Column].Name;
            var toFind = request.Parameters.Search.Value ?? "";

            var users = await _userManager.Users
                .Where(x => x.Name.Contains(toFind)
                    || x.Surname.Contains(toFind)
                    || x.Email.Contains(toFind))
                .Skip(request.Parameters.Start)
                .Take(request.Parameters.Length)
                .OrderByExtension(orderColumn, request.Parameters.Order[0].Dir)
                .Select(x => new UsersListDto() { 
                        UserName = x.UserName,
                        Id = x.Id,
                        Email = x.Email,
                        NameSurname = x.Name + " " + x.Surname,
                        IsEnabled = x.IsEnabled })
                .ToListAsync(cancellationToken);

            var total = await _userManager.Users
                .Where(x => x.Name.Contains(toFind)
                    || x.Surname.Contains(toFind)
                    || x.Email.Contains(toFind))
                .CountAsync();

            var response = new DataTablesResponse<UsersListDto>()
            {
                Draw = request.Parameters.Draw,
                RecordsFiltered = total,
                RecordsTotal = total,
                Data = users
            };

            return response;
        }
    }
}
