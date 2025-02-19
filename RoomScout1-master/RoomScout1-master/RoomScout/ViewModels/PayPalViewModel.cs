using CommunityToolkit.Mvvm.ComponentModel;
using RoomScout.Models;
using RoomScout.Services;
using System.Diagnostics;
using System.Web;
namespace RoomScout.ViewModels
{
    public partial class PayPalViewModel : ObservableObject
    {
        private readonly IPayPalService _payPalService;
        [ObservableProperty]
        private bool isLoading;
        [ObservableProperty]
        private WebViewSource webViewSource;
        public PayPalViewModel(IPayPalService payPalService)
        {
            _payPalService = payPalService;
            InitializePayment();
        }
        private async void InitializePayment()
        {
            try
            {
                IsLoading = true;
                var htmlContent = await _payPalService.GetPaymentContent(10.00m);
                WebViewSource = new HtmlWebViewSource
                {
                    Html = htmlContent
                };
                Debug.WriteLine($"HTML content loaded successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in InitializePayment: {ex}");
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
        public async Task<bool> HandleNavigationAsync(string url)
        {
            Debug.WriteLine($"Navigating to: {url}");
            if (url.Contains(PayPalConfig.SuccessUrl))
            {
                IsLoading = true;
                var paymentId = ExtractPaymentId(url);
                Debug.WriteLine($"Payment ID: {paymentId}");
                var success = await _payPalService.VerifyPayment(paymentId);
                await Shell.Current.DisplayAlert(
                    success ? "Success" : "Error",
                    success ? "Payment completed!" : "Payment verification failed",
                    "OK");
                IsLoading = false;
                return true;
            }
            if (url.Contains(PayPalConfig.ErrorUrl))
            {
                var error = HttpUtility.ParseQueryString(new Uri(url).Query)["message"];
                Debug.WriteLine($"Payment Error: {error}");
                await Shell.Current.DisplayAlert("Error", error ?? "Payment failed", "OK");
                return true;
            }
            return false;
        }
        private string ExtractPaymentId(string url)
        {
            var uri = new Uri(url);
            return HttpUtility.ParseQueryString(uri.Query)["id"];
        }
    }
}