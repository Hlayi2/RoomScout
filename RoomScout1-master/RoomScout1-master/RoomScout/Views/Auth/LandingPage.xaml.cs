using RoomScout.Services;

namespace RoomScout.Views.Auth;


public partial class LandingPage : ContentPage
{
  

    public LandingPage()
    {
        InitializeComponent();
     
    }

    

    private async void OnGetStartedClicked(object sender, EventArgs e)
    {
        // Navigate to the registration page
        // await AppShell.Current.GoToAsync("login");
        await Shell.Current.GoToAsync(nameof(LoginPage));

    }

   
}