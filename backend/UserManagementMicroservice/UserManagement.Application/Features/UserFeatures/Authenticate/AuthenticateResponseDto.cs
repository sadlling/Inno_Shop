namespace UserManagement.Application.Features.UserFeatures.Authenticate
{
    public record AuthenticateResponseDto(
        string JwtToken,
        string RefreshToken);
}
