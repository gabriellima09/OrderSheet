using OrderSheet.Core.Domain.Entities;

namespace OrderSheet.Core.Domain.Events
{
    public class ProductCreatedEvent : BaseDomainEvent
    {
        public Product ProductAdded { get; }

        public ProductCreatedEvent(Product product)
        {
            ProductAdded = product;
        }
    }
}
