using RoomScout.Interfaces;
using RoomScout.Services;
using RoomScout.Services.Auth;
using RoomScout.Views.Auth;

namespace RoomScout
{
    public partial class App : Application
    {
        public static IFirebaseAuthService AuthService { get; private set; }
        public static IFirebaseDataService DataService { get; private set; }
       
        public App()
        {
           
            InitializeComponent();

            //MainPage = new LandingPage();
            //MainPage = new NavigationPage(new LandlordDashboardPage());
            // MainPage = new NavigationPage(new BrowseListingsPage());

            AuthService = new FirebaseAuthService();
            DataService = new FirebaseDataService();


            MainPage = new AppShell();
           // MainPage = new NavigationPage(new LandingPage());

            // MainPage = new NavigationPage(new LandlordDashboardPage());
            // MainPage = new NavigationPage(new BrowseListingsPage());


        }

        private LandingPage LandingPage()
        {
            return new LandingPage();
        }

      
    }
}
