using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Features.CategoryFeatures.CreateCategory;
using ProductManagement.Application.Features.CategoryFeatures.UpdateCategory;
using ProductManagement.Application.Features.ProductFeatures.CreateProduct;
using ProductManagement.Application.Features.ProductFeatures.UpdateProduct;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Mappings
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<CreateCategoryRequest, Category>();

            CreateMap<Category, CategoryResponseDto>();

            CreateMap<UpdateCategoryRequest, Category>();

            CreateMap<CreateProductRequest, Product>()
                .ForPath(dest => dest.Category.Name, opt => opt.MapFrom(src => src.product.categoryName))
                .ForMember(dest => dest.CreatedUserId, opt => opt.MapFrom(src => src.createdUserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.product.name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.product.description))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.product.cost))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.product.isEnabled));

            CreateMap<Product, ProductResponseDto>()
                .ForPath(dest => dest.categoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<UpdateProductRequest,Product>()
                .ForPath(dest => dest.Category.Name, opt => opt.MapFrom(src => src.product.categoryName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.product.id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.product.name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.product.description))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.product.cost))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.product.isEnabled));



        }
    }
}
