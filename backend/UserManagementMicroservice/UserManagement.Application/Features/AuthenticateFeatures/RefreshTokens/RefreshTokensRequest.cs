using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.AuthenticateFeatures.RefreshTokens
{
    public record RefreshTokensRequest(
        string JwtToken,
        string RefreshToken) : IRequest<AuthenticateResponseDto>;
}
