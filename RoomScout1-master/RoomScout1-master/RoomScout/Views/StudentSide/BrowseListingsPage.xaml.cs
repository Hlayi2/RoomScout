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
        private List<RoomLocation> _allRooms;
        private RoomType? _selectedRoomType;
        private List<RoomLocation> _allListings;
        private RoomType? type;
        private readonly RoomService _roomService;


        public BrowseListingsPage()
        {
            InitializeComponent();
            _roomService = new RoomService();
            _allRooms = new List<RoomLocation>();
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
                    .Select(item => new RoomLocation
                    {
                        Id = item.Key,
                        Type = ConvertToRoomType(item.Object.RoomType),
                        Images = item.Object.Images,
                        Price = item.Object.Price.ToString("F2"),
                        location = $"{item.Object.Address?.Street},Mamontentane Gate 1 {item.Object.Address?.Suburb}",
                        Amenities = item.Object.Amenities,
                        LocationData = item.Object.Address,
                        Contact = item.Object.Contact,
                        Rules = item.Object.Rules,
                        DateAdded = item.Object.DateAdded
                    })
                    .ToList();

                _allRooms = firebaseListings;
                MainCollectionView.ItemsSource = new ObservableCollection<RoomLocation>(firebaseListings);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private RoomType ConvertToRoomType(string roomTypeString)
        {
            if (string.IsNullOrEmpty(roomTypeString))
                return RoomType.Single;

            roomTypeString = roomTypeString.Replace(" Room", "").Trim();

            if (Enum.TryParse<RoomType>(roomTypeString, true, out RoomType result))
                return result;

            return RoomType.Single;
        }

        private void OnBachelorClicked(object sender, EventArgs e) => Filter(RoomType.Bachelor);
        private void OnSharingClicked(object sender, EventArgs e) => Filter(RoomType.Sharing);
        private void OnSingleClicked(object sender, EventArgs e) => Filter(RoomType.Single);

        private void Filter(RoomType roomType)
        {
            _selectedRoomType = roomType;
            FilterListings();
        }

        private void FilterListings()
        {
            var filtered = _allRooms;

            if (_selectedRoomType.HasValue)
            {
                filtered = _allRooms
                    .Where(l => l.Type == _selectedRoomType.Value)
                    .ToList();
            }

            MainCollectionView.ItemsSource = new ObservableCollection<RoomLocation>(filtered);
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