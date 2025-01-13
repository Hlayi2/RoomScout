using RoomScout.Views.AdminSide;
using RoomScout.Views.Auth;

namespace RoomScout
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new LandingPage());
        
        // MainPage = new NavigationPage(new LandlordDashboardPage());
        // MainPage = new NavigationPage(new BrowseListingsPage());


    }
    }
}
