using RoomScout.Views.Auth;

namespace RoomScout.Views.StudentSide;

public partial class profile : ContentPage
{
	public profile()
	{
		InitializeComponent();
	}

    private async void OnUpdateProfileClicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new UpdateProfilePage());
    }

    private async void OnAppointmentClicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new Appointments());
    }

    private async void OnReportIssueClicked(object sender, EventArgs eventArgs)
    {
        await Navigation.PushAsync(new ());
    }

    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        
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

}