using AutoMapper;
using MediatR;
using ProductManagement.Application.Common.CustomExceptions;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Features.ProductFeatures.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public UpdateProductHandler(IProductRepository productRepository,ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product  = await _productRepository.GetByIdAsync(request.id);
            if (product is null)
            {
                throw new NotFoundException("Product not found");
            }
            var category = await _categoryRepository.GetByNameAsync(request.categoryName);
            if(category is null)
            {
                throw new NotFoundException("Category not found");
            }

            var productForUpdate = _mapper.Map<Product>(request);
            productForUpdate.CategoryId = category.Id;

            await _productRepository.UpdateAsync(productForUpdate);

            return Unit.Value;

            
        }
    }
}
