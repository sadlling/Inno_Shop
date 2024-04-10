using MediatR;

namespace UserManagement.Application.Features.UserFeatures.DeleteUser
{
    public record DeleteUserRequest(
        string userId):IRequest<Unit>;
}
