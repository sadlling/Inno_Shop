using AutoMapper;
using MediatR;
using System.Net.Http.Headers;
using UserManagement.Application.Common.CustomExceptions;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces.Providers;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.AuthenticateFeatures.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, AuthenticateResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        public LoginHandler(IMapper mapper, IUserRepository userRepository, ITokenProvider tokenProvider)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
        }

        public async Task<AuthenticateResponseDto> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(_mapper.Map<User>(request).UserName!);
            if (user is null)
            {
                throw new NotFoundException($"User {request.UserName} not found");
            }
            if (!await _userRepository.VerifyPassword(user,request.Password))
            {
                throw new InvalidPasswordException("Incorrect password");
            }
            if(!await _userRepository.IsEmailConfirmed(user))
            {
                throw new ConfirmEmailException("Email not confirmed");
            }
            var userRoles = await _userRepository.GetUserRolesAsync(user);
            if (userRoles is null)
            {
                throw new InvalidOperationException("Roles not found");
            }
            var jwtToken = _tokenProvider.GenerateJwtToken(user, userRoles);
            var refreshToken = _tokenProvider.GenerateRefreshToken();

            await _userRepository.UpdateRefreshTokenAsync(user, refreshToken);

            return new AuthenticateResponseDto(jwtToken, refreshToken.Token!);

        }
    }
}
