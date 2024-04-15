using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Features.ProductFeatures.CreateProduct
{
    public record CreateProductRequest(
        CreateProductDto product,
        string createdUserId):IRequest<Unit>;
}
