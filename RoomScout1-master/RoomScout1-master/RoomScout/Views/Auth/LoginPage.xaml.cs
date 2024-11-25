namespace RoomScout.Views.Auth;

using RoomScout.ViewModels.Auth;



    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }

