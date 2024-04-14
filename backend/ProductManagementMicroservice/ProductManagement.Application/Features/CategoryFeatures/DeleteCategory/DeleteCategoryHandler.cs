using AutoMapper;
using MediatR;
using ProductManagement.Application.Common.CustomExceptions;
using ProductManagement.Application.Interfaces.Repositories;

namespace ProductManagement.Application.Features.CategoryFeatures.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken =default)
        {
            var category = await _categoryRepository.GetByIdAsync(request.categoryId);
            if (category == null)
            {
                throw new NotFoundException("Category not found");
            }
            await _categoryRepository.DeleteAsync(category);
            return Unit.Value;
        }
    }
}
