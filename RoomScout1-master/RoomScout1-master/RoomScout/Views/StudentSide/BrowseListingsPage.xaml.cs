using Firebase.Database;
using RoomScout.Models.AdminSide;
using System.Collections.ObjectModel;

namespace RoomScout.Views.StudentSide
{
    public partial class BrowseListingsPage : ContentPage
    {
        private static readonly FirebaseClient firebase = new FirebaseClient("https://roomscout-a194c-default-rtdb.firebaseio.com/");
        private ObservableCollection<Listing> _listings = new();
        private ObservableCollection<Listing> _filteredListings = new();

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


                    })
                    .ToList();

                _listings = new ObservableCollection<Listing>(firebaseListings);

                _filteredListings = new ObservableCollection<Listing>(_listings);
                MainCollectionView.ItemsSource = _filteredListings;
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
            if (_listings == null || !_listings.Any()) return;

            var filtered = _listings
                .Where(l => l.RoomType.ToLower() == roomType.ToLower())
                .ToList();

            _filteredListings.Clear(); 
            foreach (var item in filtered)
            {
                _filteredListings.Add(item);
            }

            MainCollectionView.ItemsSource = _filteredListings; 

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

        private async void OnBookClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Success", "Booking request submitted!", "OK");
        }
    }
}