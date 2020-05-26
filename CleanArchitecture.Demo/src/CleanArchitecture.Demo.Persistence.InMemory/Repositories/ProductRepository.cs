using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CleanArchitecture.Demo.Application;

namespace CleanArchitecture.Demo.Persistence.InMemory.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly ConcurrentDictionary<int, DataModels.Product> _productStore = new ConcurrentDictionary<int, DataModels.Product>();

        public Product AddProduct(Product product)
        {
            Random rnd = new Random();
            var productId = rnd.Next(1, 1000);
            if (!_productStore.ContainsKey(productId))
            {
                _productStore.TryAdd(productId, new DataModels.Product()
                {
                    ExpiryInMonths = product.ExpiryInMonths,
                    ManufacturingDate = product.ManufacturingDate,
                    ProductId = productId,
                    ProductName = product.ProductName,
                    ProductCode = product.ProductKey.ProductCode,
                    ProductType = product.ProductKey.ProductType
                });
                product.SetProductId(productId);
            }
            return product;
        }

        public Product GetProduct(string productType, string productCode)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProductQuantity(string productType, string productCode, int quantity)
        {
            throw new NotImplementedException();
        }
    }

}
