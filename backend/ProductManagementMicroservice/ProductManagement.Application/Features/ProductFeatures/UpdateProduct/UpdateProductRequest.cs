using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Features.ProductFeatures.UpdateProduct
{
    public record UpdateProductRequest(
        UpdateProductDto product,
        string userId) :IRequest<Unit>;
}
