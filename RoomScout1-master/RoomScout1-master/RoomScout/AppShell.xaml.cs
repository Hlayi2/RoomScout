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

            // Auth Routes
            Routing.RegisterRoute(nameof(LandingPage), typeof(LandingPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RoomScout.Views.Auth.RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(RoomScout.Views.Auth.LoginPage));
            Routing.RegisterRoute("landlordDashboard", typeof(RoomScout.Views.AdminSide.LandlordDashboardPage));
            Routing.RegisterRoute(nameof(BrowseListingsPage), typeof(RoomScout.Views.StudentSide.BrowseListingsPage));
            Routing.RegisterRoute("nearby", typeof(RoomScout.Views.StudentSide.NearByPage));
            Routing.RegisterRoute("updateProfile", typeof(RoomScout.Views.StudentSide.UpdateProfilePage));
            Routing.RegisterRoute("appointments", typeof(RoomScout.Views.StudentSide.Appointments));
            Routing.RegisterRoute("profile", typeof(RoomScout.Views.StudentSide.profile));
            Routing.RegisterRoute("addevent", typeof(RoomScout.Views.StudentSide.AddEvent));
            Routing.RegisterRoute("register", typeof(RegisterPage));
            Routing.RegisterRoute("login", typeof(LoginPage));

            // Admin/Landlord Routes
            Routing.RegisterRoute("landlordDashboard", typeof(LandlordDashboardPage));
            Routing.RegisterRoute("listings", typeof(ManageListingsPage));
            Routing.RegisterRoute("bookings", typeof(BookingRequestsPage));
            Routing.RegisterRoute("landlordProfile", typeof(DashboardProfile));
            Routing.RegisterRoute("notifications", typeof(NotificationsPage));

            // Student Routes
            Routing.RegisterRoute("browseListings", typeof(BrowseListingsPage));
            Routing.RegisterRoute("nearby", typeof(NearByPage));     
            Routing.RegisterRoute("updateProfile", typeof(UpdateProfilePage));
            Routing.RegisterRoute("appointments", typeof(Appointments));
            Routing.RegisterRoute("profile", typeof(profile));
            Routing.RegisterRoute("addevent", typeof(AddEvent));

            Routing.RegisterRoute("listings", typeof(ManageListingsPage));
            Routing.RegisterRoute("bookings", typeof(BookingRequestsPage));
            Routing.RegisterRoute("settings", typeof(DashboardProfile));
            Routing.RegisterRoute("AddListingPage", typeof(AddListingPage));

            CurrentItem = new ShellContent
            {
                Content = new LandingPage()
            };
        }
    }
}
