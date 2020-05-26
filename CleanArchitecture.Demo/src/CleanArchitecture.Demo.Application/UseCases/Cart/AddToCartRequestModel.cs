namespace CleanArchitecture.Demo.Application
{
    public class AddToCartRequestModel : BaseUseCaseRequest
    {
        public string ProductCode { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }
    }
}
