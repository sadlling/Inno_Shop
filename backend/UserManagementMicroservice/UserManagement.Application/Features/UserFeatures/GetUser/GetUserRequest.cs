using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.UserFeatures.GetUser
{
    public record GetUserRequest
        (string userId):IRequest<UserResponseDto>;

}
