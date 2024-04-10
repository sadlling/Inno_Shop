using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.UserFeatures.GetAllUsers
{
    public record GetAllUsersRequest():IRequest<List<UserResponseDto>>;

}
