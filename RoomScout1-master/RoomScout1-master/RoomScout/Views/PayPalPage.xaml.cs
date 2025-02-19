using RoomScout.ViewModels;
using Microsoft.Maui.Controls;
namespace RoomScout.Views
{
    public partial class PayPalPage : ContentPage
    {
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
    }
}