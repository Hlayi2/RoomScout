namespace RoomScout.Views.Auth;

using RoomScout.ViewModels.Auth;



public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }

    private void OnLandlordSelected(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // If Landlord is checked
        {
            var viewModel = BindingContext as LoginViewModel;
            if (viewModel != null)
            {
                viewModel.IsTenant = false; // Uncheck Tenant
            }
        }
    }

    private void OnTenantSelected(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // If Tenant is checked
        {
            var viewModel = BindingContext as LoginViewModel;
            if (viewModel != null)
            {
                viewModel.IsLandlord = false; // Uncheck Landlord
            }
        }
    }
}

