using RoomScout.Views.AdminSide;
using RoomScout.Views.Auth;
using RoomScout.Views.StudentSide;

namespace RoomScout
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("register", typeof(RoomScout.Views.Auth.RegisterPage));
            Routing.RegisterRoute("landlordDashboard", typeof(RoomScout.Views.AdminSide.LandlordDashboardPage));
            Routing.RegisterRoute("viewApartments", typeof(RoomScout.Views.StudentSide.BrowseListingsPage));
        }
    }
}
