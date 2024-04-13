using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using UserManagement.Application.Common.CustomExceptions;
using UserManagement.Application.Common.MailHelpers;
using UserManagement.Application.Interfaces.Providers;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Features.AuthenticateFeatures.ForgotPassword
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordRequest, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailProvider _mailProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;
        public ForgotPasswordHandler(
            IUserRepository userRepository,
            IMailProvider mailProvider,
            IHttpContextAccessor httpContextAccessor,
            LinkGenerator linkGenerator)
        {
            _userRepository = userRepository;
            _mailProvider = mailProvider;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }
        public async Task<Unit> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.email);
            if (user is null)
            {
                throw new NotFoundException($"User with email {request.email} not found");
            }
            var token = await _userRepository.GetPasswordResetTokenAsync(user);

            var httpContext = _httpContextAccessor.HttpContext;
            var resetLink = _linkGenerator.GetUriByAction(httpContext!,
                action: "ResetPassword",
                controller: "Authentication",
                values: new { token, email = user.Email });

            var message = new Message(user.Email!, "Reset password link", resetLink ?? "");
            await _mailProvider.SendMailAsync(message);

            return Unit.Value;

        }
    }

}
