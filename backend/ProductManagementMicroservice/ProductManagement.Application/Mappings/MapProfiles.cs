using AutoMapper;
using ProductManagement.Application.Features.CategoryFeatures.CreateCategory;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Mappings
{
    public class MapProfiles:Profile
    {
        public MapProfiles() 
        {
            CreateMap<CreateCategoryRequest, Category>();

        }
    }
}
