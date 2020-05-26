using System;

namespace CleanArchitecture.Demo.Contracts.v1
{
    public class AddProductResponse : BaseResponse
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public int StockQuantity { get; set; }

        public int ExpiryInMonths { get; set; }
        public DateTime ManufacturingDate { get; set; }

    }
}
