using OrderSheet.Core.Domain.Entities;

namespace OrderSheet.Core.Domain.Events
{
    public class ProductChangedEvent : BaseDomainEvent
    {
        public Product Product { get; }

        public ProductChangedEvent(Product productChanged)
        {
            Product = productChanged;
        }
    }
}
