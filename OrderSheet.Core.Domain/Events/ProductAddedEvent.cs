using OrderSheet.Core.Domain.Entities;

namespace OrderSheet.Core.Domain.Events
{
    public class ProductAddedEvent : BaseDomainEvent
    {
        public Product ProductAdded { get; }

        public ProductAddedEvent(Product product)
        {
            ProductAdded = product;
        }
    }
}
