namespace UserManagement.Application.DTOs
{
    public record AuthenticateResponseDto(
        string JwtToken,
        string RefreshToken);
}
