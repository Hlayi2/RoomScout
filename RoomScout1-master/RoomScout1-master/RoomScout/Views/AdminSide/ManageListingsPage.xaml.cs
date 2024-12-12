using RoomScout.ViewModels.AdminSide;
namespace RoomScout.Views.AdminSide;

public partial class ManageListingsPage : ContentPage
{
    public ManageListingsPage()
    {
        InitializeComponent();
        BindingContext = new ManageListingsViewModel();
    }
}