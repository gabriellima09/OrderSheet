using OrderSheet.Core.Domain.Entities;
using System;

namespace OrderSheet.Core.Domain.Aggregates
{
    public class ProductItem : ValueObject
    {
        public int Quantity { get; private set; }
        public Product Product { get; private set; }

        public ProductItem(int quantity, Product product)
        {
            SetQuantity(quantity);
            Product = product ?? throw new ArgumentNullException(nameof(product));
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity > 0 ? quantity 
                : throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "The quantity must be greater than zero");
        }
    }
}
