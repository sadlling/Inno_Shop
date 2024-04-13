using MediatR;

namespace UserManagement.Application.Features.AuthenticateFeatures.ResetPassword
{
    public record ResetPasswordRequest(
        string password,
        string confirmPassword,
        string email,
        string token):IRequest<Unit>;
}
