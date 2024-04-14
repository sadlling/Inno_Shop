using MediatR;

namespace ProductManagement.Application.Features.CategoryFeatures.CreateCategory
{
    public record CreateCategoryRequest(
        string name,
        string? description):IRequest<Unit>;

}
