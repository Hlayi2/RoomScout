namespace RoomScout.Views.StudentSide;

public partial class UpdateProfilePage : ContentPage
{
	public UpdateProfilePage()
	{
		InitializeComponent();
	}

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Success", " Profile Information saved successfully! You will be redirected in 2 seconds", "OK");
        await Task.Delay(2000);

        // Navigate to the DashboardProfile page
        await Navigation.PushAsync(new profile());

        // Handle saving the form data along with the uploaded files
        // Add your save logic here
    }
}