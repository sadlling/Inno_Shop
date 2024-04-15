namespace ProductManagement.Application.DTOs
{
    public record UpdateProductDto(
        Guid id,
        string name,
        string? description,
        float cost,
        bool isEnabled,
        string categoryName);
}
