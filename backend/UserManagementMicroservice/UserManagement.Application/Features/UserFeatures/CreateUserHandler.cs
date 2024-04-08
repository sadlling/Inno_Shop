using AutoMapper;
using MediatR;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.UserFeatures
{
    public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest,Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateUserRequest request, CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(request);
            await _userRepository.CreateAsync(user);
            return Unit.Value;
        }
    }
}
