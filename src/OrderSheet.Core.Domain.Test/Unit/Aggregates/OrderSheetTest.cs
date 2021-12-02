using OrderSheet.Core.Domain.Events;
using System;
using System.Linq;
using Xunit;

namespace OrderSheet.Core.Domain.Test.Unit.Aggregates
{
    public class OrderSheetTest
    {
        [Fact]
        [Trait("Domain", "Instance")]
        public void OrderSheetInstance()
        {
            Domain.Aggregates.OrderSheet orderSheet = new Domain.Aggregates.OrderSheet();

            Assert.NotNull(orderSheet);
            Assert.True(orderSheet.Id != Guid.Empty);
            Assert.True(orderSheet.IsOpen);
            Assert.NotNull(orderSheet.Items);
            Assert.True(!orderSheet.Items.Any());
            Assert.True(orderSheet.Total == 0);
            Assert.Equal(typeof(OrderSheetOpenedEvent), orderSheet.Events.ElementAt(0).GetType());
        }

        [Fact]
        [Trait("Domain", "Behavior")]
        public void OrderSheetClosed()
        {
            Domain.Aggregates.OrderSheet orderSheet = new Domain.Aggregates.OrderSheet();

            orderSheet.Close();

            Assert.NotNull(orderSheet);
            Assert.True(orderSheet.Id != Guid.Empty);
            Assert.False(orderSheet.IsOpen);
            Assert.NotNull(orderSheet.Items);
            Assert.Equal(typeof(OrderSheetOpenedEvent), orderSheet.Events.ElementAt(0).GetType());
            Assert.Equal(typeof(OrderSheetClosedEvent), orderSheet.Events.ElementAt(1).GetType());
        }

        [Fact]
        [Trait("Domain", "Behavior")]
        public void OrderSheetAddProduct()
        {
            decimal productPrice = 10.5m;
            int productQuantity = 1;
            var product = new Entities.Product("Product", productPrice);
            Domain.Aggregates.OrderSheet orderSheet = new Domain.Aggregates.OrderSheet();

            orderSheet.AddProduct(productQuantity, product);

            Assert.NotEmpty(orderSheet.Items);
            Assert.Equal(orderSheet.Items.First().Product, product);
        }

        [Fact]
        [Trait("Domain", "Behavior")]
        public void OrderSheetAddProductThrowsArgumentNullException()
        {
            Domain.Aggregates.OrderSheet orderSheet = new Domain.Aggregates.OrderSheet();

            Assert.Throws<ArgumentNullException>(() => 
            { 
                orderSheet.AddProduct(1, null);
            });
        }

        [Fact]
        [Trait("Domain", "Behavior")]
        public void OrderSheetAddSameProduct()
        {
            decimal productPrice = 10.5m;
            int productQuantity = 1;
            var product = new Entities.Product(1, "Product", productPrice);

            int quantityToAdd = 2;

            Domain.Aggregates.OrderSheet orderSheet = new Domain.Aggregates.OrderSheet();

            for (int i = 0; i < quantityToAdd; i++)
            {
                orderSheet.AddProduct(productQuantity, product);
            }

            Assert.NotEmpty(orderSheet.Items);
            Assert.Single(orderSheet.Items);
            Assert.Equal(productQuantity * quantityToAdd, orderSheet.Items.ElementAt(0).Quantity);
        }

        [Fact]
        [Trait("Domain", "Behavior")]
        public void OrderSheetRemoveProduct()
        {
            decimal productPrice = 10.5m;
            int productQuantity = 1;
            var product = new Entities.Product("Product", productPrice);
            Domain.Aggregates.OrderSheet orderSheet = new Domain.Aggregates.OrderSheet();

            orderSheet.AddProduct(productQuantity, product);
            orderSheet.RemoveProduct(product);

            Assert.Empty(orderSheet.Items);
            Assert.Equal(0m, orderSheet.Total);
        }

        [Fact]
        [Trait("Domain", "Behavior")]
        public void OrderSheetRemoveProductThrowsException()
        {
            decimal productPrice = 10.5m;
            int productQuantity = 1;
            var product = new Entities.Product("Product", productPrice);
            Domain.Aggregates.OrderSheet orderSheet = new Domain.Aggregates.OrderSheet();

            orderSheet.AddProduct(productQuantity, product);            
            orderSheet.RemoveProduct(product);
            
            Assert.Throws<ArgumentNullException>(() => orderSheet.RemoveProduct(null));
            Assert.Throws<InvalidOperationException>(() => orderSheet.RemoveProduct(product));
        }

        [Fact]
        [Trait("Domain", "Behavior")]
        public void OrderSheetCleanitems()
        {
            decimal productPrice = 10.5m;
            int productQuantity = 1;
            var product = new Entities.Product("Product", productPrice);
            var product2 = new Entities.Product("Product", productPrice);

            Domain.Aggregates.OrderSheet orderSheet = new Domain.Aggregates.OrderSheet();

            orderSheet.AddProduct(productQuantity, product);
            orderSheet.AddProduct(productQuantity, product2);
            orderSheet.CleanItems();

            Assert.Empty(orderSheet.Items);
            Assert.Equal(0m, orderSheet.Total);
        }

        [Theory]
        [InlineData(10.5, 1)]
        [InlineData(7.12, 3)]
        [InlineData(34.77, 8)]
        [InlineData(1.99, 17)]
        [Trait("Domain", "Behavior")]
        public void OrderSheetTotalCalculation(decimal productPrice, int productQuantity)
        {
            Domain.Aggregates.OrderSheet orderSheet = new Domain.Aggregates.OrderSheet();

            orderSheet.AddProduct(productQuantity, new Entities.Product("Product", productPrice));

            Assert.NotEmpty(orderSheet.Items);
            Assert.Equal(orderSheet.Total, productPrice * productQuantity);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(8)]
        [InlineData(17)]
        [Trait("Domain", "Behavior")]
        public void OrderSheetChangeProductQuantity(int productQuantity)
        {
            Domain.Aggregates.OrderSheet orderSheet = new Domain.Aggregates.OrderSheet();
            var product = new Entities.Product("Product", 10.5m);

            orderSheet.AddProduct(1, product);
            orderSheet.ChangeProductQuantity(productQuantity, product);

            Assert.NotEmpty(orderSheet.Items);
            Assert.NotNull(orderSheet.Items.ElementAt(0).Product);
            Assert.Equal(productQuantity, orderSheet.Items.ElementAt(0).Quantity);
        }

        [Fact]
        [Trait("Domain", "Behavior")]
        public void OrderSheetChangeProductQuantityThrowsException()
        {
            var product = new Entities.Product("Product", 10m);
            Domain.Aggregates.OrderSheet orderSheet = new Domain.Aggregates.OrderSheet();

            Assert.Throws<ArgumentNullException>(() => orderSheet.ChangeProductQuantity(1, null));
            Assert.Throws<InvalidOperationException>(() => orderSheet.ChangeProductQuantity(1, product));
        }
    }
}
