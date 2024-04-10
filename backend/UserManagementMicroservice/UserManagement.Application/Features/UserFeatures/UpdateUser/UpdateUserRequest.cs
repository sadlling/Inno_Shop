using MediatR;

namespace UserManagement.Application.Features.UserFeatures.UpdateUser
{
    public record UpdateUserRequest(
        string Id,
        string UserName,
        string Email,
        string PhoneNumber
        ):IRequest<Unit>;
}
