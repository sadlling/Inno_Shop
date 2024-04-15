using MediatR;

namespace ProductManagement.Application.Features.ProductFeatures.DeleteProduct
{
    public record DeleteProductRequest(
        Guid productId,string userId):IRequest<Unit>;
}
