using MediatR;

namespace UserManagement.Application.Features.AuthenticateFeatures.ConfirmEmail
{
    public record ConfirmEmailRequest(
        string userEmail,string token):IRequest<Unit>;
}
