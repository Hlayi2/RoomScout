namespace RoomScout.Views.AdminSide;

public partial class PremiumPage : ContentPage
{
	public PremiumPage()
	{
		InitializeComponent();
	}
    private async void OnUpgradeButtonClicked(object sender, EventArgs e)
    {
        
        await Shell.Current.GoToAsync("paypal");

      
    }
}