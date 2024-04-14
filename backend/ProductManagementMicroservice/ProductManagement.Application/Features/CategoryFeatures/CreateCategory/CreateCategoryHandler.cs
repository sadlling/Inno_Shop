using AutoMapper;
using MediatR;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Features.CategoryFeatures.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var newCategory = _mapper.Map<Category>(request);
            await _categoryRepository.CreateAsync(newCategory);
            return Unit.Value;
        }
    }

}
