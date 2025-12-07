using BL.Dtos.Payment;
using BL.Services.Interfaces.IPayments;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace BL.Services.Implementation.Payments
{
    public class PaymobGateway : IPaymentGateway
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public PaymobGateway(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        private record AuthRequest(string api_key);
        private record AuthResponse(string token);

        public async Task<(string, bool)> CreateOrderAsync(CreatePaymentRequest request)
        {
            var baseUrl = _config["Paymob:BaseUrl"];
            var apiKey = _config["Paymob:ApiKey"];
            var iframeId = _config["Paymob:IframeId"];
            var integrationId = _config["Paymob:IntegrationId"];

            // ========== 1) Auth ==========
            var authReq = new AuthRequest(apiKey);
            var authResp = await _client.PostAsJsonAsync($"{baseUrl}/auth/tokens", authReq);
            if (!authResp.IsSuccessStatusCode)
            {
                var err = await authResp.Content.ReadAsStringAsync();
                return ($"AUTH_ERROR: {err}", false);
            }

            var authData = await authResp.Content.ReadFromJsonAsync<AuthResponse>();
            var token = authData?.token;
            if (string.IsNullOrEmpty(token))
                return ("AUTH_ERROR: no token", false);

            var amountInCents = ((long)(request.Amount * 100)).ToString();

            // ========== 2) Create Order ==========
            var orderBody = new
            {
                auth_token = token,
                delivery_needed = "false",
                amount_cents = amountInCents,
                currency = "EGP",
                items = Array.Empty<object>()
            };

            var orderResp = await _client.PostAsJsonAsync($"{baseUrl}/ecommerce/orders", orderBody);
            if (!orderResp.IsSuccessStatusCode)
            {
                var err = await orderResp.Content.ReadAsStringAsync();
                return ($"ORDER_ERROR: {err}", false);
            }

            var orderJson = await orderResp.Content.ReadAsStringAsync();
            using var orderDoc = JsonDocument.Parse(orderJson);
            var orderId = orderDoc.RootElement.GetProperty("id").GetInt32();

            // ========== 3) Payment Key ==========
            var paymentBody = new
            {
                auth_token = token,
                amount_cents = amountInCents,
                expiration = 3600,
                order_id = orderId,
                billing_data = new
                {
                    apartment = "NA",
                    email = "test@example.com",
                    floor = "NA",
                    first_name = "Customer",
                    last_name = "Name",
                    street = "NA",
                    building = "NA",
                    phone_number = "NA",
                    shipping_method = "NA",
                    postal_code = "NA",
                    city = "NA",
                    country = "NA",
                    state = "NA"
                },
                currency = "EGP",
                integration_id = int.Parse(integrationId),

                // أهم سطر: Paymob هيستخدمه عشان يرجّع العميل على مشروعك بعد الدفع
                redirect_url = request.RedirectUrl // ممكن تبقى null مع جيتوايز تانية
            };

            var payKeyResp = await _client.PostAsJsonAsync($"{baseUrl}/acceptance/payment_keys", paymentBody);
            if (!payKeyResp.IsSuccessStatusCode)
            {
                var err = await payKeyResp.Content.ReadAsStringAsync();
                return ($"PAYMENT_KEY_ERROR: {err}", false);
            }

            var payKeyJson = await payKeyResp.Content.ReadAsStringAsync();
            using var payKeyDoc = JsonDocument.Parse(payKeyJson);
            var paymentKey = payKeyDoc.RootElement.GetProperty("token").GetString();

            var iframeUrl = $"https://accept.paymob.com/api/acceptance/iframes/{iframeId}?payment_token={paymentKey}";
            return (iframeUrl!, true);
        }

        public Task<(string, bool)> CaptureOrderAsync(string orderId)
        {
            // Paymob مفيهوش capture زي PayPal
            return Task.FromResult(("NOT_SUPPORTED_FOR_PAYMOB", false));
        }
    }
}
