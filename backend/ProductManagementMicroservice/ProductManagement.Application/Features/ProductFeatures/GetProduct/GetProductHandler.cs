using AutoMapper;
using MediatR;
using ProductManagement.Application.Common.CustomExceptions;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces.Repositories;

namespace ProductManagement.Application.Features.ProductFeatures.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductRequest, ProductResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponseDto> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.id);
            if (product is null)
            {
                throw new NotFoundException("Product not found");
            }
            return _mapper.Map<ProductResponseDto>(product);    

        }
    }
}
