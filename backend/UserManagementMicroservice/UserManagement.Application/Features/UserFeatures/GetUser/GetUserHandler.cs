using AutoMapper;
using MediatR;
using UserManagement.Application.Common.CustomExceptions;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Features.UserFeatures.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserHandler(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> Handle(GetUserRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(request.userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return _mapper.Map<UserResponseDto>(user);
        }
    }

}
