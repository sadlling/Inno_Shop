using MediatR;

namespace UserManagement.Application.Features.UserFeatures.Authenticate
{
    public record AuthenticateUserRequest(
        string UserName,
        string Password) : IRequest<AuthenticateResponseDto>;
}
