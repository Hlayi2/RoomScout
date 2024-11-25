using RoomScout.Views.Auth;
using System.Text.RegularExpressions;

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
            string phonePattern = @"^\+?[1-9]\d{1,14}$"; // International phone number format
            return Regex.IsMatch(phone, phonePattern);
        }

        // Handle user registration
        public async Task RegisterUser()
        {
            if (!ValidateForm()) return;  // Validate form first

            // Simulate registration success (you'd replace this with actual registration logic)
            await Application.Current.MainPage.DisplayAlert("Success", "Registration Successful", "OK");

            // Navigate to the login page after registration
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }

}

