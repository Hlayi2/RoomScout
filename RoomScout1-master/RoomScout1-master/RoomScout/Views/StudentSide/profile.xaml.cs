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
        string complaint = await DisplayPromptAsync(
           "Report your Issue",
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

    private async void OnChatButtonClicked(object sender, EventArgs e)
    {
        // Navigate to the chat page
        await Navigation.PushAsync(new ChatPage());
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
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }

}