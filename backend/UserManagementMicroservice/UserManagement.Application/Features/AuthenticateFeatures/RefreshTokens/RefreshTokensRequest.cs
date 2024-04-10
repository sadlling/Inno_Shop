using MediatR;
using UserManagement.Application.Features.AuthenticateFeatures.Authenticate;

namespace UserManagement.Application.Features.AuthenticateFeatures.RefreshTokens
{
    public record RefreshTokensRequest(
        string JwtToken,
        string RefreshToken) : IRequest<AuthenticateResponseDto>;
}
