using System;

namespace CleanArchitecture
{
    public class ProductKey : IEquatable<ProductKey>
    {
        public ProductKey(string productCode, string productType)
        {
            ProductCode = productCode;
            ProductType = productType;
        }

        public string ProductCode { get; }
        public string ProductType { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProductKey);
        }

        public bool Equals(ProductKey other)
        {
            if (other == null)
                return false;

            return (string.Equals(other.ProductCode, this.ProductCode, StringComparison.OrdinalIgnoreCase)
               && string.Equals(other.ProductType, this.ProductType, StringComparison.OrdinalIgnoreCase));

        }

        public override int GetHashCode()
        {
            var hashCode = -2019929811;
            hashCode = hashCode * -1521134295 + ProductCode.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductType.GetHashCode();

            return hashCode;
        }
    }
}
