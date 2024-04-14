using MediatR;

namespace ProductManagement.Application.Features.CategoryFeatures.UpdateCategory
{
    public record UpdateCategoryRequest(
        Guid id,
        string name,
        string description):IRequest<Unit>;

}
