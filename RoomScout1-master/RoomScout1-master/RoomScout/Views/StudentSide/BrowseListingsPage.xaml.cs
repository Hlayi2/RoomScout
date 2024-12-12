namespace RoomScout.Views.StudentSide;

public partial class BrowseListingsPage : ContentPage
{
	public BrowseListingsPage()
	{
		InitializeComponent();
	}
	
	private async void OnNearByTapped(object sender, EventArgs e)

	{
		await Navigation.PushAsync(new NearByPage());
	}
	private async void OnAddEventTapped(object sender, EventArgs e)

	{
		await Navigation.PushAsync(new profile());
	}
   
  

   
    private async void OnBachelorClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BachelorPage());
    }
    private async void OnSharingClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SharingPage());
    }
    private async void OnSingleClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SinglePage());
    }

}