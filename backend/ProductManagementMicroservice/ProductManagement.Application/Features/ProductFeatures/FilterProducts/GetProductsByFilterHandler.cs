using AutoMapper;
using LinqKit;
using MediatR;
using ProductManagement.Application.Common.Filtering;
using ProductManagement.Application.Common.Paging;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Domain.Entities;
using System.Linq.Expressions;

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
            if (!request.parameters.IsValidCostRange())
            {
                throw new ArgumentException("Invalid parameters. Max cost must be less or equal min cost");
            }
            var products = await _productRepository
                .FindByConditionAsync(GetFilterExpression(request.parameters));

            if (products is null)
            {
                throw new InvalidOperationException("Failed to get all products");
            }

            return PagedList<ProductResponseDto>.ToPagedList(
                products.Select(_mapper.Map<ProductResponseDto>), request.parameters.PageNumber, request.parameters.PageSize);
        }

        private Expression<Func<Product, bool>> GetFilterExpression(ProductParameters parameters)
        {
            var filterExpression = PredicateBuilder.New<Product>(true);

            if(parameters.MinCost is not null)
            {
                filterExpression.And(product=>product.Cost >= parameters.MinCost);
            }
            if(parameters.MaxCost is not null)
            {
                filterExpression.And(product => product.Cost <= parameters.MaxCost);
            }
            if(parameters.IsEnabled is not null)
            {
                filterExpression.And(product => product.IsEnabled == parameters.IsEnabled);
            }
            if(parameters.Name is not null)
            {
                filterExpression.And(product => product.Name.Equals(parameters.Name));
            }
            return filterExpression;
        }
    }
}
