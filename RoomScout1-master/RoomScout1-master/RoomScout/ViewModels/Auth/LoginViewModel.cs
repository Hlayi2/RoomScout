using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoomScout.Views.StudentSide;

namespace RoomScout.ViewModels.Auth
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private bool isPasswordHidden = true;

        [ObservableProperty]
        private string eyeIcon = "eyeclosed.png";

        [ObservableProperty]
        private bool isLandlord;

        [ObservableProperty]
        private bool isTenant;

        private readonly Dictionary<string, (string password, string role)> userCredentials = new Dictionary<string, (string password, string role)>
        {
            { "Chantelle", ("123456", "Landlord") },
            { "Katlego", ("kat@123", "Tenant") },
            { "Marvin", ("Moagi", "Landlord") },
            { "Moloko", ("1234", "Tenant") },
        };

        [RelayCommand]
        private void TogglePassword()
        {
            IsPasswordHidden = !IsPasswordHidden;
            EyeIcon = IsPasswordHidden ? "eyeclosed.png" : "eyeopen.png";
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please enter both username and password.", "OK");
                return;
            }

            if (!userCredentials.ContainsKey(username))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Invalid username.", "OK");
                return;
            }

            var user = userCredentials[username];
            if (user.password != password)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Invalid password.", "OK");
                return;
            }

            await App.Current.MainPage.DisplayAlert("Success", $"Logged in as {user.role}.", "OK");

            if (user.role == "Landlord")
            {
                await Shell.Current.GoToAsync("///landlord");
            }
            else if (user.role == "Tenant")
            {
                await Shell.Current.GoToAsync("///student");
            }
        }

        [RelayCommand]
        private async Task ForgotPasswordAsync()
        {
            await App.Current.MainPage.DisplayAlert("Forgot Password", "Redirecting to password recovery page.", "OK");
        }

        [RelayCommand]
        private async Task GoogleLoginAsync()
        {
            await App.Current.MainPage.DisplayAlert("Google Login", "You will be redirected in 2 seconds.", "OK");
        }

        [RelayCommand]
        private async Task FacebookLoginAsync()
        {
            await App.Current.MainPage.DisplayAlert("Facebook Login", "You will be redirected in 2 seconds.", "OK");
        }

        [RelayCommand]
        private async Task NavigateToSignUpAsync()
        {
            await Shell.Current.GoToAsync("register");
        }
    }
}





