using RoomScout.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
namespace RoomScout.Services
{
    public class PayPalService : IPayPalService
    {
        public async Task<string> GetPaymentContent(decimal amount)
        {
            return await Task.FromResult($@"
<html>
    <head>
        <meta name='viewport' content='width=device-width, initial-scale=1'>
        <script src='https://www.paypal.com/sdk/js?client-id={PayPalConfig.ClientId}&currency=USD'></script>
    </head>
    <body style='margin: 0; padding: 20px;'>
        <div id='paypal-button-container'></div>
        <script>
            paypal.Buttons({{
                style: {{
                    label: 'pay',
                    tagline: false
                }},
                createOrder: (data, actions) => {{
                    return actions.order.create({{
                        purchase_units: [{{
                            amount: {{
                                value: '{amount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}'
                            }}
                        }}]
                    }});
                }},
                onApprove: async (data, actions) => {{
                    const order = await actions.order.capture();
                    window.location.href = '{PayPalConfig.SuccessUrl}?id=' + data.orderID;
                }},
                onError: (err) => {{
                    window.location.href = '{PayPalConfig.ErrorUrl}?message=' + encodeURIComponent(err.message);
                }}
            }}).render('#paypal-button-container');
        </script>
    </body>
</html>");
        }
        public async Task<bool> VerifyPayment(string paymentId)
        {
            using var client = new HttpClient();
            // Get OAuth Token
            var authBytes = Encoding.ASCII.GetBytes($"{PayPalConfig.ClientId}:{PayPalConfig.SecretKey}");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
            var response = await client.PostAsync("https://api.sandbox.paypal.com/v1/oauth2/token",
                new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("grant_type", "client_credentials") }));
            if (!response.IsSuccessStatusCode) return false;
            var authContent = await JsonSerializer.DeserializeAsync<PayPalAuthResponse>(
                await response.Content.ReadAsStreamAsync());
            if (authContent?.access_token == null) return false;
            // Verify Payment
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authContent.access_token);
            var verificationResponse = await client.GetAsync(
                $"https://api.sandbox.paypal.com/v2/checkout/orders/{paymentId}");
            return verificationResponse.IsSuccessStatusCode;
        }
        private class PayPalAuthResponse
        {
            public string access_token { get; set; }
        }
    }
}