using MediatR;
using ProductManagement.Application.Common.Filtering;
using ProductManagement.Application.Common.Paging;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Features.ProductFeatures.FilterProducts
{
    public record GetProductsByFilterQuery
        (ProductParameters parameters) : IRequest<PagedList<ProductResponseDto>>;
}
