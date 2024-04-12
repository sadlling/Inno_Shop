using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UserManagement.Application.Interfaces.Providers;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;

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
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _mailProvider = mailProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(request.user);
            await _userRepository.CreateAsync(user);
            var token = _userRepository.GetEmailConfirmationToken(user);
            
            var httpContext = _httpContextAccessor.HttpContext;


            
            return Unit.Value;
        }
    }
}
