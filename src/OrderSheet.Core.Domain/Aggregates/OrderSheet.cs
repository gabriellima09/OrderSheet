using OrderSheet.Core.Domain.Entities;
using OrderSheet.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderSheet.Core.Domain.Aggregates
{
    public class OrderSheet : BaseAggregateRoot<Guid>
    {
        public List<ProductItem> Items { get; private set; }
        public bool IsOpen { get; private set; }
        public decimal Total => CalculateTotal();

        public OrderSheet() : base(Guid.NewGuid())
        {
            Items = new List<ProductItem>();
            IsOpen = true;

            RaiseEvent(new OrderSheetOpenedEvent(this));
        }

        public void Close()
        {
            IsOpen = false;
            RaiseEvent(new OrderSheetClosedEvent(this));
        }

        public void AddProduct(int quantity, Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product), "The product that is adding not exists");

            var existingProduct = Items.FirstOrDefault(x => x.Product == product);

            if (existingProduct is null)
            {
                Items.Add(new ProductItem(quantity, product));
            }
            else
            {
                ChangeProductQuantity(existingProduct.Quantity + quantity, product);
            }
        }

        public void RemoveProduct(Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product), "The product that is removing not exists");

            if (!Items.Any(x => x.Product == product))
                throw new InvalidOperationException("The list of items of the order sheet is empty");

            Items.RemoveAll(x => x.Product == product);
        }

        public void ChangeProductQuantity(int newQuantity, Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));

            var productToEdit = Items.FirstOrDefault(x => x.Product == product);

            if (productToEdit is null)
                throw new InvalidOperationException("The product that is changing not exists in list of items");

            productToEdit.SetQuantity(newQuantity);
        }

        public void CleanItems()
        {
            Items.Clear();
        }

        private decimal CalculateTotal()
        {
            return Items is null ? decimal.Zero : Items.Sum(x => x.Product.Price * x.Quantity);
        }
    }
}
