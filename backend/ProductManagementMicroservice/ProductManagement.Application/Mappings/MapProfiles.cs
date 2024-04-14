using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Features.CategoryFeatures.CreateCategory;
using ProductManagement.Application.Features.CategoryFeatures.UpdateCategory;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Mappings
{
    public class MapProfiles:Profile
    {
        public MapProfiles() 
        {
            CreateMap<CreateCategoryRequest, Category>();

            CreateMap<Category,CategoryResponseDto>();

            CreateMap<UpdateCategoryRequest, Category>();

        }
    }
}
