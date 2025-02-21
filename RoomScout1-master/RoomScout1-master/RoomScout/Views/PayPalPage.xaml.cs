using RoomScout.ViewModels;
using Microsoft.Maui.Controls;
using RoomScout.Views.AdminSide;
namespace RoomScout.Views
{
    [QueryProperty(nameof(Amount), "amount")]
    public partial class PayPalPage : ContentPage
    {
        private decimal amount;
        public decimal Amount
        {
            get => amount;
            set
            {
                amount = value;
                if (BindingContext is PayPalViewModel vm)
                {
                    vm.InitializePayment(amount);
                }
            }
        }

        public PayPalPage(PayPalViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        public PayPalPage()
        {
        }
        private async void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (BindingContext is PayPalViewModel vm)
            {
                var shouldCancel = await vm.HandleNavigationAsync(e.Url);
                e.Cancel = shouldCancel;
            }
        }
        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (BindingContext is PayPalViewModel vm)
            {
                vm.IsLoading = false;
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(PremiumPage));


        }
    }
}