using AutoMapper;
using MediatR;
using ProductManagement.Application.Common.Paging;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces.Repositories;

namespace ProductManagement.Application.Features.ProductFeatures.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, PagedList<ProductResponseDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<ProductResponseDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            if (products is null)
            {
                throw new InvalidOperationException("Failed to get all products");
            }

            return PagedList<ProductResponseDto>.ToPagedList(
                products.Select(_mapper.Map<ProductResponseDto>),request.parameters.PageNumber,request.parameters.PageSize);

        }
    }
}
