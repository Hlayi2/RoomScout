using RoomScout.ViewModels.Auth;
using Microsoft.Maui.Controls;

namespace RoomScout.Views.Auth
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel(
                App.AuthService,
                App.DataService
            );
        }

        private void OnPasswordVisibilityToggle(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is Entry targetEntry)
            {
                targetEntry.IsPassword = !targetEntry.IsPassword;
                button.Source = targetEntry.IsPassword ? "eyeclosed.png" : "eyeopen.png";
            }
        }
        private async void OnSignInClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}