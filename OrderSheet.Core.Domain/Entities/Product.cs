using OrderSheet.Core.Domain.Events;
using System;

namespace OrderSheet.Core.Domain.Entities
{
    public class Product : BaseAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, decimal price) : base(Guid.NewGuid()) 
        {
            Name = name;
            Price = price;
        }

        private Product(Guid id) : base(id)
        {
            RaiseEvent(new ProductAddedEvent(this));
        }

        public Product(Guid id, string name, decimal price)
            : this(id)
        {
            Name = name;
            Price = price;
        }

        public void ChangePrice(decimal newPrice)
        {
            Price = newPrice;

            RaiseEvent(new ProductChangedEvent(this));
        }
    }
}
