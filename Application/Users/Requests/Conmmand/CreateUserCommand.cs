
using Application.DTOs.User;
using Application.Responses;
using MediatR;

namespace Application.Users.Requests.Conmmand
{
    public class CreateUserCommand : IRequest<BaseCommandResponse>
    {
        public CreateUserDto UserDto { get; set; }
    }
}
