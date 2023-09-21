using Assignment.Application.Common.Mappings;
using Assignment.Domain.Entities;

namespace Assignment.Application.Features.Players.Queries.GetAllProducts;

public class GetAllProductsDto : IMapFrom<Product>
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public int? Size { get; set; }
    public int? Value { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Description { get; set; }
}
