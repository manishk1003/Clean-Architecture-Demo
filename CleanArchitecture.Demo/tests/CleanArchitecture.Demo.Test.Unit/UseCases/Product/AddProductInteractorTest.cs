using System;
using System.Linq;
using CleanArchitecture.Demo.Application;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanArchitecture.Demo.Test.Unit.UseCases
{
    public class AddProductInteractorTest
    {
        private Mock<IProductRepository> _productRepository;
        private IUseCase<AddProductRequestModel, AddProductResponseModel> _addProductInteractor;

        public AddProductInteractorTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _addProductInteractor = new AddProductInteractor(_productRepository.Object);
        }

        [Fact]
        [Trait("Scenario", @"Given: Add Product is called
                When: Request model is null
                Then: It should throw exception")]
        public void Add_Product_Without_Request_Model_Should_Throw_Exception()
        {
            Action action = () => _addProductInteractor.Execute(null);
            action.Should().Throw<ArgumentException>().WithMessage($"{nameof(AddProductRequestModel)} can not be empty");

        }

        [Fact]
        [Trait("Scenario", @"Given: Add Product is called
                When: Expired product is added
                Then: It should throw ProductExpiredException")]
        public void Add_Product_Which_Is_Expired_Should_Throw_ProductExpiredException()
        {
            AddProductRequestModel addProductRequestModel = GetAddProductRequestModel();
            addProductRequestModel.ExpiryInMonths = 1;
            var productDomain = GetDomainProductFromRequestModel(addProductRequestModel);

            Action action = () => _addProductInteractor.Execute(addProductRequestModel);
            action.Should().Throw<ProductExpiredException>().WithMessage($"Product is expired");
        }

        [Fact]
        [Trait("Scenario", @"Given: Add Product is called
                When: Request model is null
                Then: It should throw ProductExpiredException")]
        public void Add_Product_With_Valid_Data_Should_Be_Successfull()
        {
            AddProductRequestModel addProductRequestModel = GetAddProductRequestModel();
            var productDomain = GetDomainProductFromRequestModel(addProductRequestModel);
            var productRepoResponse = GetDomainProductFromRequestModel(addProductRequestModel);
            productRepoResponse.SetProductId(1234);

            AddProductResponseModel expectedResponse = new AddProductResponseModel();
            expectedResponse.Product = GetDomainProductFromRequestModel(addProductRequestModel);

            _productRepository.Setup(x => x.AddProduct(It.IsAny<Product>())).Returns(productRepoResponse);
            var response = _addProductInteractor.Execute(addProductRequestModel);
            response.Should().NotBeNull();
            response.Product.Should().NotBeNull();
            response.Errors.Any().Should().Be(false);
        }

        private AddProductRequestModel GetAddProductRequestModel()
        {
            AddProductRequestModel addProductRequestModel = new AddProductRequestModel();
            addProductRequestModel.ExpiryInMonths = 6;
            addProductRequestModel.ManufacturingDate = DateTime.Now.AddMonths(-3);
            addProductRequestModel.ProductCode = "ABC123";
            addProductRequestModel.ProductName = "Demo Product";
            addProductRequestModel.ProductType = "GENERIC";
            addProductRequestModel.StockQuantity = 10;
            return addProductRequestModel;
        }

        private Product GetDomainProductFromRequestModel(AddProductRequestModel request)
        {
            var product = new Product
               (
                   new ProductKey(request.ProductCode, request.ProductType),
                   request.ProductName,
                   request.ExpiryInMonths,
                   request.ManufacturingDate,
                   request.StockQuantity
                );
            return product;
        }
    }
}
