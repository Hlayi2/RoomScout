namespace RoomScout.Views.Auth;

public partial class LandingPage : ContentPage
{
    private const string IsLandingPageViewedKey = "IsLandingPageViewed";

    public LandingPage()
    {
        InitializeComponent();
        VersionTracking.Track();

        if (!VersionTracking.IsFirstLaunchEver)
        {
            NavigateToLoginPageAsync();
        }
         
    }



    private async void NavigateToLoginPageAsync()
    {
      
       
        {
            // Navigate to the LoginPage
            await AppShell.Current.GoToAsync("//login");
        }
    }

    private async void OnGetStartedClicked(object sender, EventArgs e)
    {
        

         // Navigate to the registration page
        await AppShell.Current.GoToAsync("register");
    }
}