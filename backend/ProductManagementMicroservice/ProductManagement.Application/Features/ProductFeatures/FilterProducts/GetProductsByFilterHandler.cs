using AutoMapper;
using MediatR;
using ProductManagement.Application.Common.Paging;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces.Repositories;

namespace ProductManagement.Application.Features.ProductFeatures.FilterProducts
{
    public class GetProductsByFilterHandler : IRequestHandler<GetProductsByFilterQuery, PagedList<ProductResponseDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByFilterHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<PagedList<ProductResponseDto>> Handle(GetProductsByFilterQuery request, CancellationToken cancellationToken)
        {
            if (!request.parameters.ValidCostRange)
            {
                throw new ArgumentException("Invalid parameters. Min cost must be less or equal max cost");
            }
            var products = await _productRepository
                .FindByConditionAsync(product => product.Cost >= request.parameters.MinCost &&
                product.Cost <= request.parameters.MaxCost);

            if (products is null)
            {
                throw new InvalidOperationException("Failed to get all products");
            }

            return PagedList<ProductResponseDto>.ToPagedList(
                products.Select(_mapper.Map<ProductResponseDto>), request.parameters.PageNumber, request.parameters.PageSize);
        }
    }
}
