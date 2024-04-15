using MediatR;
using ProductManagement.Application.Common.CustomExceptions;
using ProductManagement.Application.Interfaces.Repositories;

namespace ProductManagement.Application.Features.ProductFeatures.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, Unit>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.id);
            if (product is null) 
            {
                throw new NotFoundException("Product not found");
            }
            await _productRepository.DeleteAsync(product);
            return Unit.Value;
        }
    }
}
