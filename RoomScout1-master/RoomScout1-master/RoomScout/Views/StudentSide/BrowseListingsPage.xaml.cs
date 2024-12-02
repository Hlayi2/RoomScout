namespace RoomScout.Views.StudentSide;

public partial class BrowseListingsPage : ContentPage
{
	public BrowseListingsPage()
	{
		InitializeComponent();
	}
	private async void OnViewBookingTapped(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new NearByPage());
	}
	private async void OnNearByTapped(object sender, EventArgs e)

	{
		await Navigation.PushAsync(new NearByPage());
	}
	private async void OnAddEventTapped(object sender, EventArgs e)

	{
		await Navigation.PushAsync(new NearByPage());
	}
}