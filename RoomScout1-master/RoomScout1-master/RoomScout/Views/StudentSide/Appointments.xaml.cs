using Firebase.Database;
using RoomScout.Models.AdminSide;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RoomScout.Views.StudentSide
{
    public partial class Appointments : ContentPage
    {
        private static readonly FirebaseClient firebase = new FirebaseClient("https://roomscout-a194c-default-rtdb.firebaseio.com/");
        public ObservableCollection<BookingRequest> BookingRequests { get; set; }
        public bool IsRefreshing { get; set; }
        public ICommand RefreshCommand { get; }
        private IDisposable notificationSubscription;

        public Appointments()
        {
            InitializeComponent();
            BookingRequests = new ObservableCollection<BookingRequest>();
            BindingContext = this;

            RefreshCommand = new Command(async () => await LoadBookingRequests());
            LoadBookingRequests();

            AddFirebaseListener();
            AddNotificationListener();
        }

        private void AddNotificationListener()
        {
            notificationSubscription = firebase
                .Child("notifications")
                .AsObservable<NotificationModel>()
                .Subscribe(notification =>
                {
                    MainThread.BeginInvokeOnMainThread(async () =>  // Changed to async
                    {
                        if (!notification.Object.IsRead)
                        {
                            try
                            {
                                // Show notification with animation
                                NotificationPanel.Opacity = 0;
                                NotificationPanel.IsVisible = true;
                                NotificationMessage.Text = notification.Object.Message ?? "New notification received";
                                NotificationTimestamp.Text = DateTime.Now.ToString("g");

                                // Fade in
                                await NotificationPanel.FadeTo(1, 500);

                               

                               
                             
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Notification error: {ex.Message}");
                            }
                        }
                    });
                });
        }

        private async void OnNotificationTapped(object sender, EventArgs e)
        {
            if (NotificationPanel.IsVisible)
            {
                // Fade out when tapped
                await NotificationPanel.FadeTo(0, 500);
                NotificationPanel.IsVisible = false;
            }
        }
        private void AddFirebaseListener()
        {
            firebase
                .Child("bookingRequests")
                .AsObservable<BookingRequest>()
                .Subscribe(item =>
                {
                    var updatedRequest = BookingRequests.FirstOrDefault(r => r.Key == item.Key);

                    if (updatedRequest != null)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            updatedRequest.Status = item.Object.Status;
                            updatedRequest.ConfirmationMessage = GetConfirmationMessage(item.Object);
                            updatedRequest.StatusColor = GetStatusColor(item.Object.Status);

                            // Show notification for status changes
                            NotificationPanel.Opacity = 0;
                            NotificationPanel.IsVisible = true;
                            NotificationMessage.Text = GetConfirmationMessage(item.Object);
                            NotificationTimestamp.Text = DateTime.Now.ToString("g");

                            await NotificationPanel.FadeTo(1, 500);

                         

                        });
                    }
                    else
                    {
                        BookingRequests.Add(new BookingRequest
                        {
                            Key = item.Key,
                            RoomId = item.Object.RoomId,
                            UserId = item.Object.UserId,
                            Status = item.Object.Status,
                            Timestamp = item.Object.Timestamp,
                            Name = item.Object.Name,
                            ProfilePicture = item.Object.ProfilePicture,
                            Date = item.Object.Date,
                            Time = item.Object.Time,
                            ConfirmationMessage = GetConfirmationMessage(item.Object),
                            StatusColor = GetStatusColor(item.Object.Status)
                        });
                    }
                });
        }

        private async Task LoadBookingRequests()
        {
            IsRefreshing = true;

            try
            {
                // Fetching booking requests from Firebase
                var firebaseBookingRequests = (await firebase
                    .Child("bookingRequests")
                    .OnceAsync<BookingRequest>())
                    .Select(item => new BookingRequest
                    {
                        Key = item.Key,
                        RoomId = item.Object.RoomId,
                        UserId = item.Object.UserId,
                        Status = item.Object.Status,
                        Timestamp = item.Object.Timestamp,
                        Name = item.Object.Name,
                        ProfilePicture = item.Object.ProfilePicture,
                        Date = item.Object.Date,
                        Time = item.Object.Time,
                        ConfirmationMessage = GetConfirmationMessage(item.Object),
                        StatusColor = GetStatusColor(item.Object.Status)
                    })
                    .ToList();

                // Clearing existing requests and add new ones
                BookingRequests.Clear();
                foreach (var request in firebaseBookingRequests)
                {
                    BookingRequests.Add(request);
                }

                AppointmentsCollectionView.ItemsSource = BookingRequests;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            await LoadBookingRequests();
        }

        private string GetConfirmationMessage(BookingRequest request)
        {
            if (request.Date == default || request.Time == default)
            {
                return "Booking request details are incomplete.";
            }

            if (request.Status == "Approved")
            {
                return $"Booking request accepted for {request.Date:dd/MM/yyyy} at {request.Time:hh\\:mm tt}.";
            }
            else if (request.Status == "Declined")
            {
                return $"Booking request declined for {request.Date:dd/MM/yyyy} at {request.Time:hh\\:mm tt}.";
            }
            else
            {
                return $"Booking request is pending for {request.Date:dd/MM/yyyy} at {request.Time:hh\\:mm tt}.";
            }
        }

        private Color GetStatusColor(string status)
        {
            return status switch
            {
                "Approved" => Colors.Green,
                "Declined" => Colors.Red,
                _ => Colors.Gray
            };
        }

        public class NotificationModel
        {
            public string Message { get; set; }
            public string Timestamp { get; set; }
            public bool IsRead { get; set; }
            public string UserId { get; set; }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            notificationSubscription?.Dispose();
        }
    }
}