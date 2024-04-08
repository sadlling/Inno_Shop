using MediatR;

namespace UserManagement.Application.Features.UserFeatures
{
    public record CreateUserRequest(
          string UserName,
          string Email,
          string Password,
          string PhoneNumber
          ) : IRequest<Unit>;
}
