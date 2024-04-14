using System.ComponentModel;
using ProductManagement.Domain.Common;

namespace ProductManagement.Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; } = string.Empty!;
        public string Description { get; set; } = string.Empty!;
        public double Cost { get; set; }
        public bool IsEnabled { get; set; }
        public string CreatedUserId { get; set; } = string.Empty!;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;


    }
}
