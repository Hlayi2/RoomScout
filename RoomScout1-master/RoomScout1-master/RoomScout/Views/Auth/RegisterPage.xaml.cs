using RoomScout.ViewModels.Auth;
using RoomScout.Views.AdminSide;
namespace RoomScout.Views.Auth;

public partial class RegisterPage : ContentPage
{
  

    // Constructor for the RegisterPage
    public RegisterPage()
    {
        InitializeComponent();

        BindingContext = new RegisterViewModel();

    }


    // Event handler for Register button
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        // Check if a role is selected
        if (RolePicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please select a role to register.", "OK");
            return;
        }

        string selectedRole = RolePicker.SelectedItem.ToString();

        if (selectedRole == "Register as a Landlord")
        {
            // Navigate to the Landlord Dashboard
            await Shell.Current.GoToAsync("landlordDashboard");
        }
        else if (selectedRole == "Register as a Tenant")
        {
            // Navigate to the Apartment Viewing Page
            await Shell.Current.GoToAsync("browseListings");
        }
        else
        {
            await DisplayAlert("Error", "Invalid role selected.", "OK");
        }
    }

    // Event handler for SignIn button
    private async void OnSignInClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

    // Event handler for password visibility toggle
    private void OnPasswordVisibilityToggle(object sender, EventArgs e)
    {
        if (sender is ImageButton button)
        {
            var targetEntry = button.CommandParameter as Entry;
            if (targetEntry != null)
            {
                targetEntry.IsPassword = !targetEntry.IsPassword;
                button.Source = targetEntry.IsPassword ? "eyeclosed.png" : "eyeopen.png";
            }

        }
    }
}



