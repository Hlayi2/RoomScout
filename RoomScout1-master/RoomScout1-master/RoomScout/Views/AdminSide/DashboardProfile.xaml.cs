namespace RoomScout.Views.AdminSide;
using RoomScout.Models.AdminSide;
using RoomScout.Views.Auth;

public partial class DashboardProfile : ContentPage
{
    private Landlord _landlord;

    public DashboardProfile()
	{
		InitializeComponent();

        _landlord = new Landlord
        {
            FullNames = Preferences.Get("FullNames", "John Doe"),
            ProfilePicture = Preferences.Get("ProfilePicture", "profiles.png")
        };

        BindingContext = _landlord;
    }

    private async void OnUpdateProfileClicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new LandlordDashboardPage());
    }

    private async void OnNewListingClicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new AddListingPage());
    }

    private async void OnViewListingsClicked(object sender, EventArgs e)
    {
        // Navigate to LandlordDashboardPage
        await Navigation.PushAsync(new ManageListingsPage());
    }

    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        // Display the confirmation popup
        bool isConfirmed = await DisplayAlert(
            "Confirm Logout",  // Title of the dialog
            "Are you sure you want to logout?",  // Message in the dialog
            "Yes",  // Text for the confirmation button
            "No"   // Text for the cancel button
        );

        // If the user clicked "Yes", navigate to the LoginPage
        if (isConfirmed)
        {
            // Perform logout (clear any session or authentication data here)
            // For example, you might want to clear user data or token if using authentication

            // Navigate to the LoginPage
            await Navigation.PushAsync(new LoginPage());
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
            _landlord.ContactDetails = string.Empty;
            _landlord.IdOrPassportNo = string.Empty;
            _landlord.AccommodationName = string.Empty;
            _landlord.Address = string.Empty;
            _landlord.Location = string.Empty;
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
        Preferences.Remove("user_contact");  // Example: remove the contact details

        // Clear any other stored data, such as listings, etc.
        Preferences.Remove("user_listings");  // Example: remove user's listings data
        Preferences.Remove("user_profile_picture");  // Example: remove the profile picture

    }

}