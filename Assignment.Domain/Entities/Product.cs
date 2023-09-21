using Assignment.Domain.Common;

namespace Assignment.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public int? Size { get; set; }
        public int? Value { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Description { get; set; }
    }
}
