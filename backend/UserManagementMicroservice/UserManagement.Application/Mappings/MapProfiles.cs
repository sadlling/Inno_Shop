using AutoMapper;
using UserManagement.Application.Features.AuthenticateFeatures.Authenticate;
using UserManagement.Application.Features.UserFeatures.CreateUser;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Mappings
{
    public class MapProfiles:Profile
    {
        public MapProfiles() {

            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));

            CreateMap<AuthenticateUserRequest, User>();
        }
    }
}
