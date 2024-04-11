using AutoMapper;
using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Features.UserFeatures.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, List<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserResponseDto>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetAllAsync();
            if (users is null)
            {
                throw new InvalidOperationException("All users not found exception");
            }
            return users.Select(_mapper.Map<UserResponseDto>).ToList();
        }
    }

}
