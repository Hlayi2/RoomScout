using RoomScout.Views.AdminSide;

namespace RoomScout
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            
            MainPage = new NavigationPage(new LandlordDashboardPage());
        }
    }
}
