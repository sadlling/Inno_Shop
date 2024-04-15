using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Features.ProductFeatures.GetProduct
{
    public record GetProductRequest(
        Guid id) : IRequest<ProductResponseDto>;
}
