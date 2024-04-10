﻿using AutoMapper;
using MediatR;
using System.Net.Http.Headers;
using UserManagement.Application.Common.CustomExceptions;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces.Providers;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.AuthenticateFeatures.Authenticate
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

        public async Task<AuthenticateResponseDto> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(_mapper.Map<User>(request).UserName!);
            if (user == null)
            {
                throw new NotFoundException($"User {request.UserName} not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new InvalidPasswordException("Incorrect password");
            }
            var userRoles = await _userRepository.GetUserRolesAsync(user);
            if (userRoles == null)
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
