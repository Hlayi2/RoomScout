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
        if (RolePicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please select a role to register.", "OK");
            return;
        }

        string selectedRole = RolePicker.SelectedItem.ToString();
        if (selectedRole == "Register as a Landlord")
        {
            await DisplayAlert("Success", "You have successfully registered as a Landlord", "OK");
            await Shell.Current.GoToAsync("///landlord"); // Navigate to TabBar
        }
        else if (selectedRole == "Register as a Tenant")
        {
            await DisplayAlert("Success", "You have successfully registered as a Tenant", "OK");
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



