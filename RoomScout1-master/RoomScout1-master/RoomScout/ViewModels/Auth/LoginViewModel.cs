using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoomScout.Views.StudentSide;


namespace RoomScout.ViewModels.Auth
{
    public partial class LoginViewModel : ObservableObject
    {


        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

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


        // Toggle password visibility
        [RelayCommand]
        private void TogglePassword()
        {
            IsPasswordHidden = !IsPasswordHidden;
            EyeIcon = IsPasswordHidden ? "eyeclosed.png" : "eyeopen.png";
        }

        // Login Command
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
            // Validate password
            var user = userCredentials[username];
            if (user.password != password)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Invalid password.", "OK");
                return;
            }

            // Show success message
            await App.Current.MainPage.DisplayAlert("Success", $"Logged in as {user.role}.", "OK");

            // Navigate to the respective page based on role
            if (user.role == "Landlord")
            {
                await App.Current.MainPage.Navigation.PushAsync(new Views.AdminSide.DashboardProfile());
            }
            else if (user.role == "Tenant")
            {
                await App.Current.MainPage.Navigation.PushAsync(new Views.StudentSide.BrowseListingsPage());
            }

        }
        // Forgot Password Command
        [RelayCommand]
        private async Task ForgotPasswordAsync()
        {
            await App.Current.MainPage.DisplayAlert("Forgot Password", "Redirecting to password recovery page.", "OK");
            // Implement navigation to Forgot Password page
        }

        // Google Login Command
        [RelayCommand]
        private async Task GoogleLoginAsync()
        {
            await App.Current.MainPage.DisplayAlert("Google Login", "You will be redirected in 2 seconds.", "OK");
        }

        // Facebook Login Command
        [RelayCommand]
        private async Task FacebookLoginAsync()
        {
            await App.Current.MainPage.DisplayAlert("Facebook Login", "You will be redirected in 2 seconds.", "OK");
        }

        // Navigate to Sign-Up Command
        [RelayCommand]
        private async Task NavigateToSignUpAsync()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.Auth.RegisterPage());
        }
    }
}




