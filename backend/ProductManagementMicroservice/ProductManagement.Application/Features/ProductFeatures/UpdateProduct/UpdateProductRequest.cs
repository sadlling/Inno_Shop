using MediatR;

namespace ProductManagement.Application.Features.ProductFeatures.UpdateProduct
{
    public record UpdateProductRequest(
        Guid id,
        string name,
        string? description,
        float cost,
        bool isEnabled,
        string categoryName) :IRequest<Unit>;
}
