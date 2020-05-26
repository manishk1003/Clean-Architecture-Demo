using System;

namespace CleanArchitecture.Demo.Persistence.InMemory.DataModels
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }

        public int ExpiryInMonths { get; set; }
        public DateTime ManufacturingDate { get; set; }

    }
}
