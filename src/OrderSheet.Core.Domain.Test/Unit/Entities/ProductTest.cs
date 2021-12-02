using OrderSheet.Core.Domain.Entities;
using OrderSheet.Core.Domain.Events;
using System;
using System.Linq;
using Xunit;

namespace OrderSheet.Core.Domain.Test.Unit.Entities
{
    public class ProductTest
    {
        [Fact]
        [Trait("Domain", "Instance")]
        public void ProductInstance()
        {
            Product product = new Product("Product Test", Convert.ToDecimal(new Random().Next()));
            Product productInstanced = new Product(1, "Product Test", Convert.ToDecimal(new Random().Next()));

            Assert.NotNull(product);
            Assert.NotNull(productInstanced);
            Assert.NotNull(product.Events);
            Assert.NotNull(productInstanced.Events);
            Assert.True(product.Events.Any());
            Assert.True(productInstanced.Events.Any());
            Assert.Equal(typeof(ProductCreatedEvent), product.Events.ElementAt(0).GetType());
            Assert.Equal(typeof(ProductCreatedEvent), productInstanced.Events.ElementAt(0).GetType());
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData("", -1)]
        [InlineData("", 12.4)]
        [Trait("Domain", "Instance")]
        public void ProductInstanceThrowsException(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Assert.Throws<ArgumentException>(() => new Product(name, price));
            }
            else if (price <= 0)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new Product(name, price));
            }
        }

        [Theory]
        [InlineData(1)]
        [Trait("Domain", "Behavior")]
        public void ProductChangePriceBehavior(decimal newPrice)
        {
            Product product = new Product("Product Test", Convert.ToDecimal(new Random().Next()));

            product.ChangePrice(newPrice);

            Assert.NotNull(product);
            Assert.Equal(product.Price, newPrice);
            Assert.Equal(typeof(ProductChangedEvent), product.Events.ElementAt(1).GetType());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [Trait("Domain", "Behavior")]
        public void ProductChangePriceBehaviorThrowsException(decimal newPrice)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Product product = new Product("Product Test", Convert.ToDecimal(new Random().Next()));

                product.ChangePrice(newPrice);
            });
        }
    }
}
