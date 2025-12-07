    using BL.Dtos.Payment;
    using BL.Services.Implementation.Payments;
    using BL.Services.Interfaces.IMaxMind_Ip;
    using BL.Services.Interfaces.IPayments;
    using Microsoft.AspNetCore.Mvc;
    using System.Text.Json;

    namespace WebApi.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class PaymentController : ControllerBase
        {
       
        private readonly IPaymentGateway _paymentGateway;
            private readonly StripeGateway _stripe;

            public PaymentController(PaymentGatewayFactory gateway,
                                   StripeGateway stripe,
                                   IUserCountryProvider userCountryProvider) 
            {
                var countryCode = userCountryProvider.GetCountryCode();
                _paymentGateway = gateway.GetPaymentGateway(countryCode); 
                _stripe = stripe;
            }

        ///  paypal and  paymob

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] PaymentDto data)
        {
            var request = new CreatePaymentRequest
            {
                OrderId = data.OrderId,
                Amount = data.Amount
            };

            if (_paymentGateway is PaymobGateway) // أو أي اسم الكلاس بتاعك
            {
                request.RedirectUrl = $"https://localhost:7279/Home/Paymob?shipmentId={data.OrderId}";
            }

            var result = await _paymentGateway.CreateOrderAsync(request);
            return Ok(new { id = result.Item1 });
        }



        [HttpPost("capture-order")]
            public async Task<IActionResult> CaptureOrder([FromBody] PaymentDto data)
            {
                var result = await _paymentGateway.CaptureOrderAsync(data.OrderId);
                return Ok(JsonDocument.Parse(result.Item1));
            }


            // stripe payment 
            [HttpPost("create-checkout-session")]
            public async Task<IActionResult> CreateCheckoutSession([FromBody] PaymentDto request)
            {
                // نمرّر shipmentId في success/cancel URL
                var successUrl = $"https://localhost:7279/Home/Stripe?status=success&shipmentId={request.ShipmentId}";
                var cancelUrl = $"https://localhost:7279/Home/Stripe?status=cancel&shipmentId={request.ShipmentId}";

                var sessionId = await _stripe.CreateCheckoutSessionAsync(request.Amount, successUrl, cancelUrl);

                return Ok(new { sessionId });
            }


        }
    }
