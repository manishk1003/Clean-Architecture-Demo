using CleanArchitecture.Demo.Application;
using CleanArchitecture.Demo.Contracts.v1;

namespace CleanArchitecture.Demo.WebApi.Presenters
{
    public interface IAddProductPresenter
    {
        AddProductResponse Execute(AddProductResponseModel responseMessage);
    }
}
