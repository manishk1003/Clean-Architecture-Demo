using System;

namespace CleanArchitecture.Demo.Application
{
    public class AddProductInteractor : IUseCase<AddProductRequestModel, AddProductResponseModel>
    {
        private readonly IProductRepository _productRepository;

        public AddProductInteractor(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public AddProductResponseModel Execute(AddProductRequestModel request)
        {
            if (request == null)
            {
                throw new ArgumentException($"{nameof(AddProductRequestModel)} can not be empty");
            }

            var response = new AddProductResponseModel();

            var product = new Product
                (
                    new ProductKey(request.ProductCode, request.ProductType),
                    request.ProductName,
                    request.ExpiryInMonths,
                    request.ManufacturingDate,
                    request.StockQuantity
                 );
            response.Product = product;
            if (product.IsProductExpired(DateTime.Now))
            {
                throw new ProductExpiredException("Product is expired");
            }

            var productResponse = _productRepository.AddProduct(product);

            if (productResponse.ProductId < 1)
            {
                response.Errors.Add("Not Able To Save Products");
            }
            return response;
        }
    }
}
