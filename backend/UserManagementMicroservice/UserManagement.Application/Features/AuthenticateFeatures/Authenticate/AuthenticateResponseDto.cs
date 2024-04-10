namespace UserManagement.Application.Features.AuthenticateFeatures.Authenticate
{
    public record AuthenticateResponseDto(
        string JwtToken,
        string RefreshToken);
}
