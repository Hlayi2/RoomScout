using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FirebaseAdmin.Auth.Multitenancy;
using RoomScout.Interfaces;
using System.Diagnostics; 

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
        private string selectedRole = "Tenant";

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
                // debuging logging
                Debug.WriteLine($"Starting registration process...");
                Debug.WriteLine($"Selected Role: {SelectedRole}");

                if (Password != ConfirmPassword)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Passwords don't match", "OK");
                    return;
                }

                IsRegistering = true;
                Debug.WriteLine("Registering with Firebase...");

                var authResult = await _authService.RegisterWithEmailAndPasswordAsync(
                    Email, Password, SelectedRole);

                Debug.WriteLine($"Firebase registration successful. User ID: {authResult.User.LocalId}");

                await SecureStorage.SetAsync("uid", authResult.User.LocalId);
                await SecureStorage.SetAsync("userRole", SelectedRole);

                // getting Actual role
               // var role = SelectedRole.Split('/')[0]; // This will take "Tenant" from "Tenant/Landlord"
               // Debug.WriteLine($"Parsed Role: {role}");

                var route = selectedRole == "Landlord" ? "//landlord" : "//tenant";

                Debug.WriteLine($"Attempting navigation to route: {route}");

                try
                {
                    await Shell.Current.GoToAsync("..", true); // Go back to clear stack
                    await Shell.Current.GoToAsync(route);
                    Debug.WriteLine("Navigation successful");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Navigation failed: {ex.Message}");
                    Debug.WriteLine($"Navigation stack trace: {ex.StackTrace}");
                    await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Registration error: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsRegistering = false;
                Debug.WriteLine("Registration process completed");
            }
        }
    }
}