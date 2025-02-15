using RoomScout.ViewModels.Auth;
using RoomScout.Services.Auth;
using Microsoft.Maui.Controls;

namespace RoomScout.Views.Auth
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        }
    }
}