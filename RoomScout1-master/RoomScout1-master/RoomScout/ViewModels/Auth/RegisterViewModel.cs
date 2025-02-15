using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoomScout.Interfaces;
using RoomScout.Models;
using RoomScout.Models.Enums;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace RoomScout.ViewModels.Auth
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IFirebaseAuthService _authService;
        private readonly IFirebaseDataService _dataService;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string confirmPassword = string.Empty;

        [ObservableProperty]
        private string phoneNumber = string.Empty;

        [ObservableProperty]
        private string selectedRole = "Tenant";

        [ObservableProperty]
        private bool isRegistering;

        [ObservableProperty]
        private bool isPasswordVisible;

        [ObservableProperty]
        private bool isConfirmPasswordVisible;

        [ObservableProperty]
        private string eyeIcon = "eyeclosed.png";

        public ObservableCollection<string> AvailableRoles { get; } = new ObservableCollection<string>
        {
            "Tenant",
            "Landlord"
        };

        public RegisterViewModel(IFirebaseAuthService authService, IFirebaseDataService dataService)
        {
            _authService = authService;
            _dataService = dataService;
        }

        [RelayCommand]
        private async Task NavigateToLogin()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void TogglePasswordVisibility()
        {
            IsPasswordVisible = !IsPasswordVisible;
            EyeIcon = IsPasswordVisible ? "eyeopen.png" : "eyeclosed.png";
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (IsRegistering) return;

            if (!ValidateInputs())
                return;

            try
            {
                IsRegistering = true;

                var authResult = await _authService.RegisterWithEmailAndPasswordAsync(
                    Email.Trim(),
                    Password
                );

                var request = new RegisterRequest
                {
                    Email = Email.Trim(),
                    PhoneNumber = PhoneNumber.Trim(),
                    Role = SelectedRole == "Landlord" ? UserRole.Landlord : UserRole.Tenant,
                    CreatedAt = DateTime.UtcNow
                };

                await _dataService.CreateUserProfileAsync(authResult.User.LocalId, request);
                await SecureStorage.SetAsync("uid", authResult.User.LocalId);
                await SecureStorage.SetAsync("userRole", SelectedRole);

                var route = SelectedRole == "Landlord" ? "///landlord" : "///tenant";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Registration Error", GetUserFriendlyErrorMessage(ex.Message), "OK");
            }
            finally
            {
                IsRegistering = false;
            }
        }

        private bool ValidateInputs()
        {
            // Email validation
            if (string.IsNullOrWhiteSpace(Email))
            {
                ShowError("Please enter your email address.");
                return false;
            }

            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(Email, emailPattern))
            {
                ShowError("Please enter a valid email address.");
                return false;
            }

            // Phone validation
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                ShowError("Please enter your phone number.");
                return false;
            }

            var phonePattern = @"^\+?[\d\s-]{10,}$";
            if (!Regex.IsMatch(PhoneNumber, phonePattern))
            {
                ShowError("Please enter a valid phone number.");
                return false;
            }

            // Password validation
            if (string.IsNullOrWhiteSpace(Password))
            {
                ShowError("Please enter a password.");
                return false;
            }

            if (Password.Length < 6)
            {
                ShowError("Password must be at least 6 characters long.");
                return false;
            }

            if (Password != ConfirmPassword)
            {
                ShowError("Passwords do not match.");
                return false;
            }

            // Role validation
            if (string.IsNullOrWhiteSpace(SelectedRole))
            {
                ShowError("Please select a role.");
                return false;
            }

            return true;
        }

        private async void ShowError(string message)
        {
            await App.Current.MainPage.DisplayAlert("Error", message, "OK");
        }

        private string GetUserFriendlyErrorMessage(string errorMessage)
        {
            return errorMessage switch
            {
                var msg when msg.Contains("EMAIL_EXISTS") =>
                    "This email is already registered. Please use a different email or try logging in.",
                var msg when msg.Contains("INVALID_EMAIL") =>
                    "Please enter a valid email address.",
                var msg when msg.Contains("WEAK_PASSWORD") =>
                    "Please choose a stronger password. It should be at least 6 characters long.",
                var msg when msg.Contains("OPERATION_NOT_ALLOWED") =>
                    "Account creation is currently disabled. Please try again later.",
                var msg when msg.Contains("TOO_MANY_ATTEMPTS_TRY_LATER") =>
                    "Too many attempts. Please try again later.",
                _ => "An error occurred during registration. Please try again."
            };
        }
    }
}