namespace BL.Services.Interfaces.IPayments
{
    public interface IPaymentGatewayFactory
    {
        IPaymentGateway GetPaymentGateway(string Countrycode);
    }
}
