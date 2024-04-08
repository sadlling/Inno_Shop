
namespace UserManagement.Application.DTOs
{
    public record CreateUserDto(
        string UserName,
        string Email,
        string Password,
        string PhoneNumber
        );
    
    public record UpdateUserDto(
        string UserName,
        string Email,
        string PhoneNumber
        );
}
