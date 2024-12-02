namespace RoomScout.Views.AdminSide;

public partial class LandlordProfile : ContentPage
{
	public LandlordProfile()
	{
		InitializeComponent();
	}

    private async void OnUpdateProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LandlordDashboardPage());
    }

    private async void OnAddListingClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddListingPage());
    }

    private async void OnViewListingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageListingsPage());
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
        if (confirm)
        {
            // Implement logout logic here
            await Navigation.PopToRootAsync();
        }
    }

    private async void OnDeleteAccountClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Delete Account", "Are you sure you want to delete your account? This action cannot be undone.", "Yes", "No");
        if (confirm)
        {
            // Implement account deletion logic here
            await DisplayAlert("Deleted", "Your account has been deleted.", "OK");
            await Navigation.PopToRootAsync();
        }
    }

    private async void OnReportIssueClicked(object sender, EventArgs e)
    {
       // await Navigation.PushAsync(new ReportIssuePage());
    }
}

