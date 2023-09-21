using Assignment.Domain.Common;
using Assignment.Domain.Entities;

namespace Assignment.Application.Features.Players.Commands.UpdatePlayer;

public class ProductUpdatedEvent : BaseEvent
{
    public Product Product { get; }

    public ProductUpdatedEvent(Product product)
    {
        Product = product;
    }
}
