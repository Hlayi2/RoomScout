using Firebase.Database;
using RoomScout.Models.AdminSide;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using RoomScout.Views.AdminSide;
using RoomScout.Models.Location;

namespace RoomScout.Views.StudentSide
{
    public partial class BrowseListingsPage : ContentPage
    {
        private static readonly FirebaseClient firebase = new FirebaseClient("https://roomscout-a194c-default-rtdb.firebaseio.com/");
        private ObservableCollection<Listing> _listings = new();
        private string _selectedRoomType;

        public BrowseListingsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadListings();
        }

        private async Task LoadListings()
        {
            try
            {
                var firebaseListings = (await firebase
                    .Child("listings")
                    .OnceAsync<Listing>())
                    .Select(item => new Listing
                    {
                        Key = item.Key,
                        RoomType = item.Object.RoomType,
                        Images = item.Object.Images,
                        Price = item.Object.Price,
                       Address = item.Object.Address,
                        Amenities = item.Object.Amenities 


                    })
                    .ToList();

                _listings = new ObservableCollection<Listing>((IEnumerable<Listing>)firebaseListings);
                MainCollectionView.ItemsSource = _listings;
              
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

      

        private void OnBachelorClicked(object sender, EventArgs e) => Filter("Bachelor");
        private void OnSharingClicked(object sender, EventArgs e) => Filter("Sharing");
        private void OnSingleClicked(object sender, EventArgs e) => Filter("Single");


        private void Filter(string roomType)
        { 
            _selectedRoomType = roomType;
            FilterListings();
        }

        private void FilterListings ()
        {
            var filtered = _listings
                 .Where(l => l.RoomType == _selectedRoomType)
                 .ToList();
            

            MainCollectionView.ItemsSource = new ObservableCollection<Listing>(filtered);
        }

        private async void OnViewBookingTapped(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ViewBooking());
        }

        private async void OnNearByTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NearByPage());
        }

        private async void OnAddEventTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new profile());
        }

        private void OnFilterImageClicked(object sender, EventArgs e)
        {
            buttonStack.IsVisible = !buttonStack.IsVisible;

        }

        private async void OnBookClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var room = button?.BindingContext as Listing;

            if (room != null)
            {
                var bookingRequest = new BookingRequest
                {
                    RoomId = room.Key,
                    UserId = "studentUserId", 
                    Status = "pending",
                    Timestamp = DateTime.UtcNow
                };

                try
                {
                    await firebase
                        .Child("bookingRequests")
                        .PostAsync(bookingRequest);

                    await DisplayAlert("Success", "Booking request submitted!", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

    }
}