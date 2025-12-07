using BL.Dtos.Payment;

namespace BL.Services.Interfaces.IPayments
{
    public interface IPaymentGateway
    {
        Task<(string, bool)> CreateOrderAsync(CreatePaymentRequest request);
        Task<(string, bool)> CaptureOrderAsync(string orderId);
    }
}
