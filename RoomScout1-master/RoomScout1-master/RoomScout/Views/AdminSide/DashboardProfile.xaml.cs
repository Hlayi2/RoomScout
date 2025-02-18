namespace RoomScout.Views.AdminSide;
using RoomScout.Models.AdminSide;
using RoomScout.ViewModels.AdminSide;
using RoomScout.Views.Auth;
using System.Diagnostics;

public partial class DashboardProfile : ContentPage
{
    private Landlord _landlord;

    public DashboardProfile()
	{
		InitializeComponent();

        _landlord = new Landlord
        {
            FullNames = Preferences.Get("FullNames", "John Doe"),
            ProfilePicture = Preferences.Get("ProfilePicture", "prof.png")
        };

        BindingContext = _landlord;
    }


    private async void OnUpdateProfileClicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new LandlordDashboardPage());
    }

    private async void OnImageButtonClicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new ChatbotPage());
    }


    private async void OnReportTechnicalIssueClicked(object sender, EventArgs e)
    {
        // Show a pop-up to input the complaint
        string complaint = await DisplayPromptAsync(
            "Report Technical Issue",
            "Please describe the issue:",
            "Submit",
            "Cancel",
            "Enter your complaint here",
            maxLength: 500,
            keyboard: Keyboard.Text);

        // Check if the user submitted the complaint
        if (!string.IsNullOrWhiteSpace(complaint))
        {
            // Send the complaint (you can replace this with your logic to send the complaint)
            bool isSent = await SendComplaintAsync(complaint);

            // Notify the user
            if (isSent)
            {
                await DisplayAlert("Success", "Your complaint has been submitted.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to submit your complaint. Please try again.", "OK");
            }
        }
    }
    private async Task<bool> SendComplaintAsync(string complaint)
    {
        // Simulate sending the complaint (replace this with your actual logic)
        await Task.Delay(1000); // Simulate network delay
        return true; // Return true if the complaint was sent successfully
    }

   
    private async void OnProClicked(object sender, EventArgs eventArgs)
    {
        await Navigation.PushAsync(new PremiumPage());
    }

    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        try
        {
            // Display the confirmation popup
            bool isConfirmed = await DisplayAlert(
                "Confirm Logout",  // Title of the dialog
                "Are you sure you want to logout?",  // Message in the dialog
                "Yes",  // Text for the confirmation button
                "No"   // Text for the cancel button
            );

            // If the user clicked "Yes", logout and navigate to login page
            if (isConfirmed)
            {
                // Clear secure storage
                SecureStorage.RemoveAll();

                // Use Shell navigation to return to login page
                await Shell.Current.GoToAsync("//login");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Logout error: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");

            // Fallback to resetting the application if navigation fails
            await DisplayAlert("Error", "An error occurred during logout. The app will restart.", "OK");
            Application.Current.MainPage = new AppShell();
        }
    }

    private async void OnDeleteAccountButtonClicked(object sender, EventArgs e)
    {
        // Display the confirmation popup
        bool isConfirmed = await DisplayAlert(
            "Confirm Action", 
            "Are you sure you want to delete your account? This action cannot be undone.",  // Message
            "Yes",  
            "Cancel" 
        );

        // If the user clicked "Yes", proceed with deletion
        if (isConfirmed)
        {
            // Clear all user data (example with Preferences/SecureStorage)
            ClearUserData();

            // Clear the Landlord data
            _landlord.FullNames = string.Empty;
            _landlord.Email = string.Empty;
            _landlord.IdOrPassportNo = string.Empty;
            _landlord.AccommodationName = string.Empty;
            _landlord.Address = string.Empty;
            _landlord.ProfilePicture = string.Empty;

            // Clear the BindingContext to update the UI (optional)
            BindingContext = null;

            // Navigate back to the LandingPage
            await Navigation.PushAsync(new LandingPage());
            // This will take the user to the root page (LandingPage)
        }
    }

    // Method to clear all user data
    private void ClearUserData()
    {
        // Clear user data from Preferences or SecureStorage
        SecureStorage.Remove("user_token");  // Example: remove stored token
        Preferences.Remove("user_full_name");  // Example: remove the full name
        Preferences.Remove("user_email");  // Example: remove the email

        // Clear any other stored data, such as listings, etc.
        Preferences.Remove("user_listings");  // Example: remove user's listings data
        Preferences.Remove("user_profile_picture");  // Example: remove the profile picture

    }

}