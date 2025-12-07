using BL.Services.Interfaces.IPayments;
using Microsoft.Extensions.DependencyInjection;

namespace BL.Services.Implementation.Payments
{
    public class PaymentGatewayFactory : IPaymentGatewayFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentGatewayFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentGateway GetPaymentGateway(string countryCode)
        {
            if (countryCode == "EG")
                return _serviceProvider.GetRequiredService<PaymobGateway>();
            else
                return _serviceProvider.GetRequiredService<PayPalGateway>();
        }
    }
}
