using FluentValidation;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces.Repositories;

namespace ProductManagement.Application.Validators
{
    public class CreateProductValidator:AbstractValidator<CreateProductDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateProductValidator(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;

            RuleFor(product => product.name)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(product => product.description)
                .NotEmpty()
                .MaximumLength(100);
        
            RuleFor(product=>product.cost)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(product => product.isEnabled)
               .NotEmpty();
            RuleFor(product => product.categoryName)
                .NotEmpty()
                .MustAsync(IsExistCategoryName).WithMessage("Category not exists");
        }
        private async Task<bool> IsExistCategoryName(string categoryName, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetByNameAsync(categoryName);
            return category is not null;
        }
    }
}
