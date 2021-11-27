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

        public void AddProduct(ProductItem product)
        {
            Items.Add(product);
        }

        public void RemoveProduct(ProductItem product)
        {
            Items.Remove(product);
        }

        public void ChangeProductQuantity(int newQuantity, ProductItem product)
        {
            var productToEdit = Items.FirstOrDefault(x => x.Product.Equals(product));

            if (productToEdit is null)
                throw new Exception();

            productToEdit.SetQuantity(newQuantity);
        }

        public void CleanProducts()
        {
            Items.Clear();
        }

        private decimal CalculateTotal()
        {
            return Items == null ? decimal.Zero : Items.Sum(x => x.Product.Price * x.Quantity);
        }
    }
}
