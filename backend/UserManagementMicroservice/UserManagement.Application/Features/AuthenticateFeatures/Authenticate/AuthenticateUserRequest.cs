using MediatR;

namespace UserManagement.Application.Features.AuthenticateFeatures.Authenticate
{
    public record AuthenticateUserRequest(
        string UserName,
        string Password) : IRequest<AuthenticateResponseDto>;
}
