using System;

namespace CleanArchitecture.Demo.Application
{
    public class AddToCartInteractor : IUseCase<AddToCartRequestModel, AddToCartResponseModel>
    {
        private readonly IProductRepository _productRepository;

        public AddToCartInteractor(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public AddToCartResponseModel Execute(AddToCartRequestModel requestModel)
        {
            if (requestModel == null)
            {
                throw new ArgumentException($"{nameof(AddToCartRequestModel)} can not be empty");
            }

            AddToCartResponseModel addToCartResponseModel = new AddToCartResponseModel();

            if (requestModel.Quantity <= 0)
            {
                addToCartResponseModel.Errors.Add("Quantity should be greater than 0");
                return addToCartResponseModel;
            }

            var productDomain = _productRepository.GetProduct(requestModel.ProductType, requestModel.ProductCode);

            if (productDomain == null)
            {
                addToCartResponseModel.Errors.Add("Product does not exists in the system");
                return addToCartResponseModel;
            }

            if (productDomain.StockQuantity < requestModel.Quantity)
            {
                throw new ProductOutOfStockException($"Product currently is having {productDomain.StockQuantity} quantity available");
                // Raise Add Product Stock Reminder as Events
            }

            int remainingQuantity = productDomain.StockQuantity - requestModel.Quantity;

            // If Remaining Quantity is less than threshold quanity Raise Events


            bool isQuantityUpdated = _productRepository.UpdateProductQuantity(requestModel.ProductType, requestModel.ProductCode, remainingQuantity);
            return addToCartResponseModel;
        }
    }
}
