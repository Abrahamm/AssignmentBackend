using Assignment.Domain.Common;
using Assignment.Domain.Entities;

namespace Assignment.Application.Features.Players.Commands.DeletePlayer;

public class ProductDeletedEvent : BaseEvent
{
    public Product Product { get; }

    public ProductDeletedEvent(Product product)
    {
        Product = product;
    }
}
