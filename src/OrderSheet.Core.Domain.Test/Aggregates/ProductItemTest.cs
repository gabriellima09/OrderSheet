using OrderSheet.Core.Domain.Aggregates;
using OrderSheet.Core.Domain.Entities;
using System;
using Xunit;

namespace OrderSheet.Core.Domain.Test.Aggregates
{
    public class ProductItemTest
    {
        [Fact]
        [Trait("Domain", "Instance")]
        public void ProductItemInstance()
        {
            ProductItem productItem = new ProductItem(1, new Product("Product", 10.5m));

            Assert.NotNull(productItem);
            Assert.True(productItem.Quantity > 0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [Trait("Domain", "Instance")]
        public void ProductItemInstanceThrowsArgumentOutOfRangeException(int quantity)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ProductItem(quantity, new Product("Product", 10.5m)));
        }

        [Fact]
        [Trait("Domain", "Instance")]
        public void ProductItemInstanceThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductItem(1, null));
        }
    }
}
