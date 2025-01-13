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

            Routing.RegisterRoute(nameof(LandingPage), typeof(LandingPage));
            Routing.RegisterRoute("register", typeof(RoomScout.Views.Auth.RegisterPage));
            Routing.RegisterRoute("login", typeof(RoomScout.Views.Auth.LoginPage));
            Routing.RegisterRoute("landlordDashboard", typeof(RoomScout.Views.AdminSide.LandlordDashboardPage));
            Routing.RegisterRoute("browseListings", typeof(RoomScout.Views.StudentSide.BrowseListingsPage));
            Routing.RegisterRoute("nearby", typeof(RoomScout.Views.StudentSide.NearByPage));
            Routing.RegisterRoute("updateProfile", typeof(RoomScout.Views.StudentSide.UpdateProfilePage));
            Routing.RegisterRoute("appointments", typeof(RoomScout.Views.StudentSide.Appointments));
            Routing.RegisterRoute("profile", typeof(RoomScout.Views.StudentSide.profile));
            Routing.RegisterRoute("addevent", typeof(RoomScout.Views.StudentSide.AddEvent));

            CurrentItem = new ShellContent
            {
                Content = new LandingPage()
            };
        }



       
    }
}
