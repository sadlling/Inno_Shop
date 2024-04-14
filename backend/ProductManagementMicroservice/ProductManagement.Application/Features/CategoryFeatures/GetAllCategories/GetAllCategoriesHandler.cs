using AutoMapper;
using MediatR;
using ProductManagement.Application.Common.Paging;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces.Repositories;

namespace ProductManagement.Application.Features.CategoryFeatures.GetAllCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesRequest, PagedList<CategoryResponseDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<CategoryResponseDto>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync();
            if(categories is null)
            {
                throw new InvalidOperationException("Failed to get all categories");
            }
            return PagedList<CategoryResponseDto>
                .ToPagedList(categories.Select(_mapper.Map<CategoryResponseDto>),request.parameters.PageNumber,request.parameters.PageSize);
        }
    }

}
