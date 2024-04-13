using MediatR;

namespace UserManagement.Application.Features.AuthenticateFeatures.ForgotPassword
{
    public record ForgotPasswordRequest(string email):IRequest<Unit>;

}
