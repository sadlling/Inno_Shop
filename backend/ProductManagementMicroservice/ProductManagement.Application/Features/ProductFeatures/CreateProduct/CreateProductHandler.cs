using AutoMapper;
using MediatR;
using ProductManagement.Application.Common.CustomExceptions;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Features.ProductFeatures.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CreateProductHandler(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Product>(request);

            var category = await _categoryRepository.GetByNameAsync(request.product.categoryName);
            if (category is null) 
            {
                throw new NotFoundException("Category for product not found");
            }

            newProduct.CategoryId = category.Id;
            await _productRepository.CreateAsync(newProduct);
            return Unit.Value;

        }
    }
}
