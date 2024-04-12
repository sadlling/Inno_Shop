using MediatR;
using UserManagement.Application.Features.UserFeatures.CreateUser;

namespace UserManagement.Application.Features.AuthenticateFeatures.Register
{
    public record RegisterRequest(
        CreateUserRequest user):IRequest<Unit>;


}
