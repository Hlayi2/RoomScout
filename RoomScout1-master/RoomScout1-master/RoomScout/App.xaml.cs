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
            AuthService = new FirebaseAuthService();
            DataService = new FirebaseDataService();

            MainPage = new AppShell();
         

        }

      

      
    }
}
