using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Features.CategoryFeatures.GetCategory
{
    public record GetCategoryRequest(
        Guid id):IRequest<CategoryResponseDto>;

}
