using System;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Demo.Test.Unit.Domain
{
    public class ProductTests
    {
        [Fact]
        [Trait("Scenario", @"Given: Product is going to be created
                When: No data is provided out of the mandatory fields
                Then: It should throw exception")]
        public void Create_Product_With_No_ProductKey_Should_Throw_Exception()
        {
            Action action = () => new Product(null, null, 0, DateTime.MinValue, 0);
            action.Should().Throw<ArgumentException>().WithMessage($"{nameof(Product.ProductKey)} can not be empty");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Scenario", @"Given: Product is going to be created
                When: Product Name  is not provided
                Then: It should throw exception")]
        public void Create_Product_With_No_ProductName_Should_Throw_Exception(string productName)
        {
            ProductKey productKey = new ProductKey("ABC123", "GENERICS");
            Action action = () => new Product(productKey, productName, 0, DateTime.MinValue, 0);
            action.Should().Throw<ArgumentException>().WithMessage($"{nameof(Product.ProductName)} can not be empty");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [Trait("Scenario", @"Given: Product is going to be created
                When: Expiry in months is less than equal to zero
                Then: It should throw exception")]
        public void Create_Product_With_Invalid_Expiry_Should_Throw_Exception(int expiryInMonths)
        {
            ProductKey productKey = new ProductKey("ABC123", "GENERICS");
            string productName = "Demo Product";
            Action action = () => new Product(productKey, productName, expiryInMonths, DateTime.MinValue, 0);
            action.Should().Throw<ArgumentException>().WithMessage($"{nameof(Product.ExpiryInMonths)} should be greater than 0");
        }


        [Fact]
        [Trait("Scenario", @"Given: Product is going to be created
                When: Invalid Manufacturing Date
                Then: It should throw exception")]
        public void Create_Product_With_Invalid_ManufacturingDate_Should_Throw_Exception()
        {
            ProductKey productKey = new ProductKey("ABC123", "GENERICS");
            string productName = "Demo Product";
            int expiryInMonths = 6;
            Action action = () => new Product(productKey, productName, expiryInMonths, DateTime.MinValue, 0);
            action.Should().Throw<ArgumentException>().WithMessage($"{nameof(Product.ManufacturingDate)} should be valid");
        }

        [Fact]
        [Trait("Scenario", @"Given: Product is going to be created
                When: All the mandatory valid data is provided
                Then: It should be successful")]
        public void Create_Product_With_Valid_Data_Should_Be_Successful()
        {
            ProductKey productKey = new ProductKey("ABC123", "GENERICS");
            string productName = "Demo Product";
            int expiryInMonths = 6;
            int stockQuantity = 10;
            DateTime manufacturingDate = DateTime.Now.AddMonths(-3);
            var product = new Product(productKey, productName, expiryInMonths, manufacturingDate, stockQuantity);
            product.Should().NotBeNull();
            product.ProductKey.Should().Be(productKey);
            product.ProductName.Should().Be(productName);
            product.ManufacturingDate.Should().Be(manufacturingDate);
            product.ExpiryInMonths.Should().Be(expiryInMonths);
            product.StockQuantity.Should().Be(stockQuantity);
        }

        [Fact]
        [Trait("Scenario", @"Given: Is Product is Expired is called
                When: Date time is passed before expiry 
                Then: It should be successful")]
        public void IsProductExpired_Should_Return_True_If_Product_Has_Passed_Expiry()
        {
            ProductKey productKey = new ProductKey("ABC123", "GENERICS");
            string productName = "Demo Product";
            int expiryInMonths = 2;
            int stockQuantity = 10;
            DateTime manufacturingDate = DateTime.Now.AddMonths(-3);
            var product = new Product(productKey, productName, expiryInMonths, manufacturingDate, stockQuantity);
            product.IsProductExpired(DateTime.Now).Should().Be(true);
        }

        [Fact]
        [Trait("Scenario", @"Given: Is Product is Expired is called
                When: Date time is passed before expiry 
                Then: It should be successful")]
        public void IsProductExpired_Should_Return_False_If_Product_Has_Not_Passed_Expiry()
        {
            ProductKey productKey = new ProductKey("ABC123", "GENERICS");
            string productName = "Demo Product";
            int expiryInMonths = 4;
            int stockQuantity = 10;
            DateTime manufacturingDate = DateTime.Now.AddMonths(-3);
            var product = new Product(productKey, productName, expiryInMonths, manufacturingDate, stockQuantity);
            product.IsProductExpired(DateTime.Now).Should().Be(false);
        }
    }

}
