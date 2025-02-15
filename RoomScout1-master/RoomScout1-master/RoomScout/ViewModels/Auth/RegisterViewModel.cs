using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoomScout.Interfaces;

namespace RoomScout.ViewModels.Auth
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IFirebaseAuthService _authService;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string phoneNumber = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string confirmPassword = string.Empty;

        [ObservableProperty]
        private string selectedRole = "Tenant/Landlord";

        [ObservableProperty]
        private bool isRegistering;

        public RegisterViewModel(IFirebaseAuthService authService, IFirebaseDataService dataService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (IsRegistering) return;

            try
            {
                if (Password != ConfirmPassword)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Passwords don't match", "OK");
                    return;
                }

                IsRegistering = true;
                var authResult = await _authService.RegisterWithEmailAndPasswordAsync(
                    Email, Password, SelectedRole);

                await SecureStorage.SetAsync("uid", authResult.User.LocalId);
                await SecureStorage.SetAsync("userRole", SelectedRole);


                var route = SelectedRole == "Landlord" 
                    ? "//landlord" 
                    : "//tenant";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsRegistering = false;
            }
        }
    }
}