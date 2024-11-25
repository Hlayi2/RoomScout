namespace RoomScout.Views.AdminSide;
using RoomScout.Models.AdminSide;

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
}