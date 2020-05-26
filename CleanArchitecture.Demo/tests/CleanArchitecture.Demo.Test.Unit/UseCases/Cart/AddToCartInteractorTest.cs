using System;
using System.Linq;
using CleanArchitecture.Demo.Application;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanArchitecture.Demo.Test.Unit.UseCases
{

    public class AddToCartInteractorTest
    {
        private Mock<IProductRepository> _productRepository;
        private IUseCase<AddToCartRequestModel, AddToCartResponseModel> _addToCartInteractor;

        public AddToCartInteractorTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _addToCartInteractor = new AddToCartInteractor(_productRepository.Object);
        }

        [Fact]
        [Trait("Scenario", @"Given: Add To Cart is called
                When: Request model is null
                Then: It should throw exception")]
        public void Add_Product_Without_Request_Model_Should_Throw_Exception()
        {
            Action action = () => _addToCartInteractor.Execute(null);
            action.Should().Throw<ArgumentException>().WithMessage($"{nameof(AddToCartRequestModel)} can not be empty");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [Trait("Scenario", @"Given: Add To Cart is called
                When: Request Quantity for Product is less than equal to 0
                Then: It should throw exception")]
        public void Add_Product_With_Invalid_Quantity_Should_Give_Errors_In_Response(int quantity)
        {
            AddToCartRequestModel addToCartRequestModel = GetAddToCartRequestModel();
            addToCartRequestModel.Quantity = quantity;

            AddToCartResponseModel expectedResponse = new AddToCartResponseModel();
            var errMsg = "Quantity should be greater than 0";
            expectedResponse.Errors.Add(errMsg);
            var response = _addToCartInteractor.Execute(addToCartRequestModel);
            response.Should().NotBeNull();
            response.Errors.Any().Should().Be(true);
            response.Errors.First().Should().Be(errMsg);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Scenario", @"Given: Add To Cart is called
                When: Invalid Product Type 
                Then: It should throw exception")]
        public void Add_Product_With_Invalid_ProductType_Should_Give_Errors_In_Response(string productType)
        {
            AddToCartRequestModel addToCartRequestModel = GetAddToCartRequestModel();
            addToCartRequestModel.ProductType = productType;

            AddToCartResponseModel expectedResponse = new AddToCartResponseModel();
            var errMsg = "Product does not exists in the system";
            expectedResponse.Errors.Add(errMsg);

            _productRepository.Setup(x => x.GetProduct(It.IsAny<string>(), It.IsAny<string>())).Returns((Product)null);

            var response = _addToCartInteractor.Execute(addToCartRequestModel);
            response.Should().NotBeNull();
            response.Errors.Any().Should().Be(true);
            response.Errors.First().Should().Be(errMsg);
        }


        //Similarly other scenarios will be covered by writing test cases here

        private AddToCartRequestModel GetAddToCartRequestModel()
        {
            return new AddToCartRequestModel()
            {
                ProductCode = "ABC123",
                ProductType = "GENERIC",
                Quantity = 100
            };
        }
    }
}
