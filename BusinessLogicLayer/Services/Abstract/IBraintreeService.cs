namespace BusinessLogicLayer.Services.Abstract;

public interface IBraintreeService
{
    IBraintreeGateway CreateGateway();
    IBraintreeGateway GetGateway();
}
