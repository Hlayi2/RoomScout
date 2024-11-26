namespace RoomScout.Views.Auth;

public partial class LandingPage : ContentPage
{
    public LandingPage()
    {
        InitializeComponent();
    }
    private async void OnGetStartedClicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync("register");
    }
}