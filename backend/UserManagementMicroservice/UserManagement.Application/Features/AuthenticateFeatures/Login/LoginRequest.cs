using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.AuthenticateFeatures.Login
{
    public record LoginRequest(
        string UserName,
        string Password) : IRequest<AuthenticateResponseDto>;
}
