using AutoMapper;
using MediatR;
using UserManagement.Application.Common.CustomExceptions;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Features.UserFeatures.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public DeleteUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            await _userRepository.DeleteAsync(user);
            return Unit.Value;
        }
    }
}
