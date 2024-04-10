namespace UserManagement.Application.DTOs
{
    public record UserResponseDto(
        string Id,
        string UserName,
        string Email,
        string PhoneNumber,
        bool EmailConfirmed
        );
}
