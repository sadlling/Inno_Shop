using FluentValidation;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Features.ProductFeatures.CreateProduct;
using ProductManagement.Application.Interfaces.Repositories;

namespace ProductManagement.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateProductValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(req => req.product.name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(20);

            RuleFor(req => req.product.description)
                .MinimumLength(1)
                .MaximumLength(100);

            RuleFor(req => req.product.cost)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(req => req.product.isEnabled)
               .NotEmpty();
            RuleFor(req => req.product.categoryName)
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
