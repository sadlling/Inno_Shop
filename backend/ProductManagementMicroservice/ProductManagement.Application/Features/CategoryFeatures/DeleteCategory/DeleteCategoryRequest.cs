using MediatR;

namespace ProductManagement.Application.Features.CategoryFeatures.DeleteCategory
{
    public record DeleteCategoryRequest(
        Guid categoryId):IRequest<Unit>;
}
