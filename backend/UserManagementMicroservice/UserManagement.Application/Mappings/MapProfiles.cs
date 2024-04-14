using AutoMapper;
using UserManagement.Application.DTOs;
using UserManagement.Application.Features.AuthenticateFeatures.Login;
using UserManagement.Application.Features.UserFeatures.CreateUser;
using UserManagement.Application.Features.UserFeatures.UpdateUser;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Mappings
{
    public class MapProfiles:Profile
    {
        public MapProfiles() {

            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<LoginRequest, User>();

            CreateMap<User, UserResponseDto>();
            
            CreateMap<UpdateUserRequest, User>();

        }
    }
}
