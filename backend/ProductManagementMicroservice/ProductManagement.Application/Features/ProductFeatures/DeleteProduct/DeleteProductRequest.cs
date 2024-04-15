using MediatR;

namespace ProductManagement.Application.Features.ProductFeatures.DeleteProduct
{
    public record DeleteProductRequest(Guid id):IRequest<Unit>;
}
