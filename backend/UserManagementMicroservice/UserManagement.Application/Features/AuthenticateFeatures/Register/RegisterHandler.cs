using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using UserManagement.Application.Common.MailHelpers;
using UserManagement.Application.Interfaces.Providers;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;
using Microsoft.AspNetCore.Routing;

namespace UserManagement.Application.Features.AuthenticateFeatures.Register
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMailProvider _mailProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;
        public RegisterHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IMailProvider mailProvider,
            IHttpContextAccessor httpContextAccessor,
            LinkGenerator linkGenerator)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _mailProvider = mailProvider;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }

        public async Task<Unit> Handle(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(request.user);
            await _userRepository.CreateAsync(user);
            var token = await _userRepository.GetEmailConfirmationToken(user);
            
            var httpContext = _httpContextAccessor.HttpContext;
            var confirmationLink = _linkGenerator.GetUriByAction(httpContext!,
                action:"ConfirmEmail",
                controller: "Authentication",
                values: new { token ,email =user.Email });

            var message = new Message(request.user.Email, "Confirmation email link", confirmationLink ?? "");
            await _mailProvider.SendMailAsync(message);

            return Unit.Value;
        }
    }
}
