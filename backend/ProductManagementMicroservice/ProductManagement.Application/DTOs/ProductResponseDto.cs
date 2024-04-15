namespace ProductManagement.Application.DTOs
{
    public record ProductResponseDto(
        Guid id,
        string name,
        string description,
        string cost,
        bool isEnabled,
        string createdUserId,
        string categoryName,
        DateTimeOffset updatedAt);
}
