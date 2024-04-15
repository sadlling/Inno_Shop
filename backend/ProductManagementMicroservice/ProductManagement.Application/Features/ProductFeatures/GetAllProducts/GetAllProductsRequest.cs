using MediatR;
using ProductManagement.Application.Common.Paging;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Features.ProductFeatures.GetAllProducts
{
    public record GetAllProductsRequest(
        QueryStringParameters parameters):IRequest<PagedList<ProductResponseDto>>;
}
