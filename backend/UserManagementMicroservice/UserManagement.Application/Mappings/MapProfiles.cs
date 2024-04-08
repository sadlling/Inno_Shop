using AutoMapper;
using UserManagement.Application.Features.UserFeatures;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Mappings
{
    public class MapProfiles:Profile
    {
        public MapProfiles() {

            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
               





        }
    }
}
