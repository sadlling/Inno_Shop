namespace ProductManagement.Domain.Common
{
    public class BaseEntity
    {
        Guid Id { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
    }
}
