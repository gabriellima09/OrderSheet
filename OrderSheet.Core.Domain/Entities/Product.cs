using OrderSheet.Core.Domain.Events;

namespace OrderSheet.Core.Domain.Entities
{
    public class Product : BaseAggregateRoot<int>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        private Product(int id) : base(id)
        {
            RaiseEvent(new ProductAddedEvent(this));
        }

        public Product(int id, string name, decimal price)
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
