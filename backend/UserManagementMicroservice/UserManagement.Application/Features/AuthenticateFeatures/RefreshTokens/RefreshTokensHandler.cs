using MediatR;
using System.Security.Claims;
using UserManagement.Application.Features.AuthenticateFeatures.Authenticate;
using UserManagement.Application.Interfaces.Providers;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Common;

namespace UserManagement.Application.Features.AuthenticateFeatures.RefreshTokens
{
    public class RefreshTokensHandler : IRequestHandler<RefreshTokensRequest, AuthenticateResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;

        public RefreshTokensHandler(IUserRepository userRepository, ITokenProvider tokenProvider)
        {
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
        }
        public async Task<AuthenticateResponseDto> Handle(RefreshTokensRequest request, CancellationToken cancellationToken)
        {
            var principal = _tokenProvider.GetPrincipalFromExpiredToken(request.JwtToken);
            if (principal == null)
            {
                throw new InvalidOperationException("Principal not found");
            }
            var userId = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var userForUpdate = await _userRepository.GetByIdAsync(userId);
            if (userForUpdate == null || userForUpdate.RefreshToken != request.RefreshToken || userForUpdate.TokenExpires < DateTime.Now)
            {
                throw new Exception("Refresh token expiried or user not found");
            }
            var userRoles = await _userRepository.GetUserRolesAsync(userForUpdate);

            var newJwtToken = _tokenProvider.GenerateJwtToken(userForUpdate, userRoles);
            var newRefreshToken = _tokenProvider.GenerateRefreshToken();

            await _userRepository.UpdateRefreshTokenAsync(userForUpdate, newRefreshToken);

            return new AuthenticateResponseDto(newJwtToken, newRefreshToken.Token!);
        }
    }
}
