using Application.DTOs.User;
using MediatR;

namespace Application.Users.Requests.Queris
{
    public class GetUsersRequest : IRequest<List<UsersListDto>>
    {

    }
}
