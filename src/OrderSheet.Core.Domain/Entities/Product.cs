using OrderSheet.Core.Domain.Events;
using System;

namespace OrderSheet.Core.Domain.Entities
{
    public class Product : BaseAggregateRoot<int>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, decimal price)
        {
            Name = string.IsNullOrWhiteSpace(name) ? 
                throw new ArgumentException("The name must not be null or empty")
                : name;
            Price = price <= 0 ?
                throw new ArgumentOutOfRangeException("The price must not be less than or equals to zero")
                : price;
            RaiseEvent(new ProductCreatedEvent(this));
        }

        private Product(int id) : base(id)
        {
            RaiseEvent(new ProductCreatedEvent(this));
        }

        public Product(int id, string name, decimal price)
            : this(id)
        {
            Name = string.IsNullOrWhiteSpace(name) ?
                throw new ArgumentException("The name must not be null or empty")
                : name;
            Price = price <= 0 ?
                throw new ArgumentOutOfRangeException("The price must not be less than or equals to zero")
                : price;
        }

        public void ChangePrice(decimal newPrice)
        {
            Price = Price = newPrice <= 0 ?
                throw new ArgumentOutOfRangeException("The price must not be less than or equals to zero")
                : newPrice;

            RaiseEvent(new ProductChangedEvent(this));
        }
    }
}
