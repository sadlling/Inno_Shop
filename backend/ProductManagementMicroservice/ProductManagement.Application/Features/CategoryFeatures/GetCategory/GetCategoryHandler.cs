using AutoMapper;
using MediatR;
using ProductManagement.Application.Common.CustomExceptions;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces.Repositories;

namespace ProductManagement.Application.Features.CategoryFeatures.GetCategory
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryRequest, CategoryResponseDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDto> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.id);
            if (category is null)
            {
                throw new NotFoundException("Failed to get category");
            }
            return _mapper.Map<CategoryResponseDto>(category);
        }
    }

}
