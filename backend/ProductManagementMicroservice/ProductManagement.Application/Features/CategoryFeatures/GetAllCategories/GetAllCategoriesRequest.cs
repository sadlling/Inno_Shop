using MediatR;
using ProductManagement.Application.Common.Paging;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Features.CategoryFeatures.GetAllCategories
{
    public record GetAllCategoriesRequest(
        QueryStringParameters parameters):IRequest<PagedList<CategoryResponseDto>>;

}
