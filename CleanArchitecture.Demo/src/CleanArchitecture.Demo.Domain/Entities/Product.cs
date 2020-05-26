using System;

namespace CleanArchitecture
{
    public class Product
    {
        public Product(ProductKey productKey, string productName, int expiryInMonths, DateTime manufacturingDate, int stockQuantity)
        {
            ProductKey = productKey ?? throw new ArgumentException($"{nameof(ProductKey)} can not be empty");
            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new ArgumentException($"{nameof(ProductName)} can not be empty");
            }
            if (expiryInMonths <= 0)
            {
                throw new ArgumentException($"{nameof(ExpiryInMonths)} should be greater than 0");
            }
            if (manufacturingDate == DateTime.MinValue)
            {
                throw new ArgumentException($"{nameof(ManufacturingDate)} should be valid");
            }
            ProductName = productName;
            ExpiryInMonths = expiryInMonths;
            ManufacturingDate = manufacturingDate;
            StockQuantity = stockQuantity;
        }

        public int ProductId { get; private set; }

        public ProductKey ProductKey { get; private set; }
        public string ProductName { get; private set; }

        public int ExpiryInMonths { get; private set; }

        public DateTime ManufacturingDate { get; private set; }

        public int StockQuantity { get; set; }

        public bool IsProductExpired(DateTime dateTime)
        {
            TimeSpan ts = ManufacturingDate.AddMonths(ExpiryInMonths) - dateTime;
            return ts.Days < 0;
        }

        public void SetProductId(int productId) => ProductId = productId;

    }
}
