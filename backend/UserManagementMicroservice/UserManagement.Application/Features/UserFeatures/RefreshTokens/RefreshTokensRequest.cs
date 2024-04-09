using MediatR;
using UserManagement.Application.Features.UserFeatures.Authenticate;

namespace UserManagement.Application.Features.UserFeatures.RefreshTokens
{
    public record RefreshTokensRequest(
        string JwtToken,
        string RefreshToken):IRequest<AuthenticateResponseDto>;
}
