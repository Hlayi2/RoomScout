using CommunityToolkit.Mvvm.Input;
using RoomScout.Views.Auth;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace RoomScout.ViewModels.Auth
{
    public partial class RegisterViewModel : BindableObject
    {


        // Bindable properties
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }

        private bool _isPasswordHidden = true;

        private bool _isConfirmPasswordHidden = true;

        private string _passwordEyeIcon = "eyeclosed.png";

        private string _confirmPasswordEyeIcon = "eyeclosed.png";


        public bool IsPasswordHidden
        {
            get => _isPasswordHidden;
            set
            {
                _isPasswordHidden = value;
                OnPropertyChanged(nameof(IsPasswordHidden));
                EyeIcon = _isPasswordHidden ? "eyeclosed.png" : "eyeopen.png";
            }
        }

        public string EyeIcon
        {
            get => _passwordEyeIcon;
            set
            {
                _passwordEyeIcon = value;
                OnPropertyChanged(nameof(EyeIcon));
            }
        }

        public bool IsConfirmPasswordHidden
        {
            get => _isConfirmPasswordHidden;
            set
            {
                _isConfirmPasswordHidden = value;
                OnPropertyChanged(nameof(IsConfirmPasswordHidden));
                ConfirmPasswordEyeIcon = _isConfirmPasswordHidden ? "eyeclosed.png" : "eyeopen.png";
            }
        }

        public string ConfirmPasswordEyeIcon
        {
            get => _confirmPasswordEyeIcon;
            set
            {
                _confirmPasswordEyeIcon = value;
                OnPropertyChanged(nameof(ConfirmPasswordEyeIcon));
            }
        }

        // Commands for toggling visibility
        public ICommand TogglePasswordVisibilityCommand { get; }
        public ICommand ToggleConfirmPasswordVisibilityCommand { get; }

        // Constructor
        public RegisterViewModel()
        {
            TogglePasswordVisibilityCommand = new RelayCommand(() => IsPasswordHidden = !IsPasswordHidden);
            ToggleConfirmPasswordVisibilityCommand = new RelayCommand(() => IsConfirmPasswordHidden = !IsConfirmPasswordHidden);
        }
        // Validate the form data
        public bool ValidateForm()
        {
            // Validate Email
            if (string.IsNullOrEmpty(Email) || !IsValidEmail(Email))
            {
                Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid email address.", "OK");
                return false;
            }

            // Validate Phone
            if (string.IsNullOrEmpty(Phone) || !IsValidPhone(Phone))
            {
                Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid phone number.", "OK");
                return false;
            }

            // Validate Password match
            if (Password != ConfirmPassword)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                return false;
            }

            // Validate Password strength
            if (string.IsNullOrEmpty(Password) || Password.Length < 6)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Password must be at least 6 characters.", "OK");
                return false;
            }

            return true;
        }

        // Validate email format using regex
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Validate phone number format using regex
        private bool IsValidPhone(string phone)
        {
            if (!phone.StartsWith("+27"))
            {
                phone = "+27" + phone.TrimStart('0'); 
            }

            string phonePattern = @"^\+27\d{9}$"; 
            return Regex.IsMatch(phone, phonePattern);
        }

        // Handle user registration
        public async Task RegisterUser()
        {
            if (!ValidateForm()) return;  // Validate form first

            // Simulate registration success 
            await Application.Current.MainPage.DisplayAlert("Success", "Registration Successful", "OK");

            // Navigate to the login page after registration
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }

}

