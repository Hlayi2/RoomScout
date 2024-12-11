using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoomScout.Views;


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
            if (isLandlord && isTenant)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please select a role(Landlord or Tenant).", "OK");
                return;

            }

            bool isAuthenticated = username == "test" && password == "password";

            if (isAuthenticated)
            {
                string role = isLandlord ? "Landlord" : "Tenant";
                await App.Current.MainPage.DisplayAlert("success", $"Logged in as{role}.", "OK");

                //Navigate to the respective dashboard
                if (isLandlord)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new Views.AdminSide.DashboardProfile());
                }
                else if (isTenant)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new Views.StudentSide.BrowseListingsPage());

                }

            }
            else

            {
                await App.Current.MainPage.DisplayAlert("Error", "Invalid username or password.", "OK");

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
            await App.Current.MainPage.DisplayAlert("Google Login", "Google login functionality not implemented yet.", "OK");
        }

        // Facebook Login Command
        [RelayCommand]
        private async Task FacebookLoginAsync()
        {
            await App.Current.MainPage.DisplayAlert("Facebook Login", "Facebook login functionality not implemented yet.", "OK");
        }

        // Navigate to Sign-Up Command
        [RelayCommand]
        private async Task NavigateToSignUpAsync()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.Auth.RegisterPage());
        }

        public void OnIsLandlordSelected(bool value)
        {
            if (value) 
            {
            
                IsTenant = false;  //Uncheck Landlord when Tenant is selected
            }
        
        }
    }
}
    

