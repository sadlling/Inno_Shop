namespace ProductManagement.Application.DTOs
{
    public record CreateProductDto(string name,
        string? description,
        float cost,
        bool isEnabled,
        string categoryName);
}
