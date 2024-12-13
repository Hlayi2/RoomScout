using RoomScout.Models.Location;
using RoomScout.ViewModels.StudentSide;
using RoomType = RoomScout.Models.Location.RoomType;

namespace RoomScout.Views.StudentSide
{
    public partial class BrowseListingsPage : ContentPage
    {
        private readonly BrowseListingsViewModel _viewModel;

        public BrowseListingsPage(BrowseListingsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        public BrowseListingsPage()
        {
        }

        // Update your existing click handlers to use the ViewModel
        private void OnBachelorClicked(object sender, EventArgs e)
        {
            _viewModel.FilterRoomsCommand.Execute(RoomType.Bachelor);
        }

        private void OnSharingClicked(object sender, EventArgs e)
        {
            _viewModel.FilterRoomsCommand.Execute(RoomType.Sharing);
        }

        private void OnSingleClicked(object sender, EventArgs e)
        {
            _viewModel.FilterRoomsCommand.Execute(RoomType.Single);
        }

        // Keep your existing navigation handlers
        private async void OnNearByTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NearByPage());

        }

        private async void OnAddEventTapped(object sender, EventArgs e)
        {
            // Your existing navigation code
        }
    }
}