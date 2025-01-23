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

            CurrentItem = new ShellContent
            {
                Content = new LandingPage()
            };
        }
    }
}
