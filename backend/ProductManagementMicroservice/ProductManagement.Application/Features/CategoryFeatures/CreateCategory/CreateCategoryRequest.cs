using MediatR;

namespace ProductManagement.Application.Features.CategoryFeatures.CreateCategory
{
    public record CreateCategoryRequest(
        string Name,
        string? Description):IRequest<Unit>;

}
