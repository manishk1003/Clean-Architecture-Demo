namespace CleanArchitecture.Demo.Application
{
    public interface IUseCase<TUseCaseRequest, TUseCaseResponse> where TUseCaseRequest : BaseUseCaseRequest where TUseCaseResponse : BaseUseCaseResponse
    {
        TUseCaseResponse Execute(TUseCaseRequest baseUseCaseRequest);
    }
}
