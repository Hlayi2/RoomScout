using System.Threading.Tasks;

namespace RoomScout.Services
{
    public interface IPayPalService
    {
        Task<string> GetPaymentContent(decimal amount);
        Task<bool> VerifyPayment(string paymentId);
    }
}