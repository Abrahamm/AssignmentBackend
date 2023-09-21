using Assignment.Domain.Common;
using Assignment.Domain.Entities;

namespace Assignment.Application.Features.Players.Commands.CreatePlayer
{
    public class ProductCreatedEvent : BaseEvent
    {
        public Product Product { get; }

        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
    }
}
