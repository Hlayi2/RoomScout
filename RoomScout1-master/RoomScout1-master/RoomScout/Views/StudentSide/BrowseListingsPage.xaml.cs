using RoomScout.Models.Location;
using RoomScout.ViewModels.Auth;
using RoomScout.ViewModels.StudentSide;
using RoomType = RoomScout.Models.Location.RoomType;

namespace RoomScout.Views.StudentSide
{
    public partial class BrowseListingsPage : ContentPage
    {
       

        public BrowseListingsPage()
        {
            InitializeComponent();
        }

        private async void OnViewBookingTapped(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewBooking());
        }


        // Update your existing click handlers to use the ViewModel
        private void OnBachelorClicked(object sender, EventArgs e)
        {
          
        }

        private void OnSharingClicked(object sender, EventArgs e)
        {
           
        }

        private void OnSingleClicked(object sender, EventArgs e)
        {
           
        }

        // Keep your existing navigation handlers
        private async void OnNearByTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NearByPage());

        }

        private async void OnAddEventTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new profile());
        }

        private async void OnBookClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Success", "Booking request submitted!", "OK");
        }

    }
}