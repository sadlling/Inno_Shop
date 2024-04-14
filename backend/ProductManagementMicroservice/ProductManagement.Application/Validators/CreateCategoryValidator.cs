using FluentValidation;
using ProductManagement.Application.Features.CategoryFeatures.CreateCategory;
using ProductManagement.Application.Interfaces.Repositories;

namespace ProductManagement.Application.Validators
{
    public class CreateCategoryValidator:AbstractValidator<CreateCategoryRequest>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryValidator(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;

            RuleFor(category => category.Name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(20)
                .MustAsync(IsUniqueCategoryName).WithMessage("Category name already exists");

            RuleFor(category => category.Description)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(100);
        }
        private async Task<bool> IsUniqueCategoryName(string categoryName, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetByNameAsync(categoryName);
            return category == null;
        }

    }
}
