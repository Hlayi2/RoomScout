using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoomScout.Interfaces;
using RoomScout.Views.Auth;
using System.Threading.Tasks;

namespace RoomScout.ViewModels.Auth
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IFirebaseAuthService _authService;
        private readonly IFirebaseDataService _dataService;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private bool isLoggingIn;

        [ObservableProperty]
        private bool isPasswordHidden = true;

        [ObservableProperty]
        private string eyeIcon = "eyeclosed.png";

        public LoginViewModel(IFirebaseAuthService authService, IFirebaseDataService dataService)
        {
            _authService = authService;
            _dataService = dataService;
        }

        [RelayCommand]
        private void TogglePassword()
        {
            IsPasswordHidden = !IsPasswordHidden;
            EyeIcon = IsPasswordHidden ? "eyeclosed.png" : "eyeopen.png";
        }

        [RelayCommand]
        private async Task NavigateToSignUp()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            if (IsLoggingIn) return;

            if (!ValidateInputs())
                return;

            try
            {
                IsLoggingIn = true;
                var authResult = await _authService.SignInWithEmailAndPasswordAsync(Email.Trim(), Password);
                await SecureStorage.SetAsync("uid", authResult.User.LocalId);
                var profile = await _dataService.GetUserProfileAsync(authResult.User.LocalId);
                await SecureStorage.SetAsync("userRole", profile.Role);
                var route = profile.Role == "Landlord" ? "///landlord" : "///tenant";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsLoggingIn = false;
            }
        }

        [RelayCommand]
        private async Task ForgotPasswordAsync()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please enter your email", "OK");
                return;
            }
            try
            {
                await _authService.SendPasswordResetEmailAsync(Email);
                await App.Current.MainPage.DisplayAlert("Success", "Password reset email sent", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                App.Current.MainPage.DisplayAlert("Error", "Please enter your email", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                App.Current.MainPage.DisplayAlert("Error", "Please enter your password", "OK");
                return false;
            }

            return true;
        }
    }
}