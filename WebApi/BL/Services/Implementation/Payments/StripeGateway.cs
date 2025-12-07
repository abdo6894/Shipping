using Microsoft.Extensions.Configuration;
using Stripe.Checkout;

namespace BL.Services.Implementation.Payments
{
   
    public class StripeGateway
    {
        private readonly IConfiguration _config;

        public StripeGateway(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> CreateCheckoutSessionAsync(decimal amount, string successUrl, string cancelUrl)
        {
            var amountInCents = (long)(amount * 100);

            var options = new SessionCreateOptions
            {
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Quantity = 1,
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            UnitAmount = amountInCents,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Shipping order"
                            }
                        }
                    }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Id; 
        }
    }
}

