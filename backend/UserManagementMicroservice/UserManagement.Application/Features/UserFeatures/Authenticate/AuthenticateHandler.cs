using AutoMapper;
using MediatR;
using UserManagement.Application.Interfaces.Providers;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Features.UserFeatures.Authenticate
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateUserRequest, AuthenticateResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        public AuthenticateHandler(IMapper mapper, IUserRepository userRepository, ITokenProvider tokenProvider)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
        }

        public Task<AuthenticateResponseDto> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
