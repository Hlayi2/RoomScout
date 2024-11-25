namespace RoomScout.Views.AdminSide;

public partial class DashboardProfile : ContentPage
{
	public DashboardProfile()
	{
		InitializeComponent();
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
}