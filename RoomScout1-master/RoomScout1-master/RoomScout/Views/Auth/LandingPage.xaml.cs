namespace RoomScout.Views.Auth;

public partial class LandingPage : ContentPage
{
    private const string IsLandingPageViewedKey = "IsLandingPageViewed";

    public LandingPage()
    {
        InitializeComponent();
       
    }



   

    private async void OnGetStartedClicked(object sender, EventArgs e)
    {
         // Navigate to the registration page
        // await AppShell.Current.GoToAsync("login");
        await Navigation.PushAsync(new LoginPage());
    }
}