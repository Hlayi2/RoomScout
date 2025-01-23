using RoomScout.Views.Auth;

namespace RoomScout
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
     
        }

        private LandingPage LandingPage()
        {
            return new LandingPage();
        }
    }
}
