namespace ProductManagement.Application.DTOs
{
    public record CategoryResponseDto(
        Guid id,
        string name,
        string description,
        DateTimeOffset createdAt);
}
