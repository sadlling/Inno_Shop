using AutoMapper;
using MediatR;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.UserFeatures.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            await _userRepository.UpdateAsync(user);
            return Unit.Value;

        }
    }
}
