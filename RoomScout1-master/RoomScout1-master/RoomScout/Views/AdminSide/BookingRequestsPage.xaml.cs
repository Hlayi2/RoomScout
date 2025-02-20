using System.Collections.ObjectModel;
using Firebase.Database;
using Firebase.Database.Query;
using RoomScout.Models.AdminSide;
using RoomScout.ViewModels.AdminSide;

namespace RoomScout.Views.AdminSide;

public partial class BookingRequestsPage : ContentPage
{
    private static readonly FirebaseClient firebase = new FirebaseClient("https://roomscout-a194c-default-rtdb.firebaseio.com/");
    public ObservableCollection<BookingRequest> BookingRequests { get; set; }

    public BookingRequestsPage()
    {
        InitializeComponent();
        BookingRequests = new ObservableCollection<BookingRequest>();
        BindingContext = this;
        LoadBookingRequests();
    }

    private async void LoadBookingRequests()
    {
        try
        {
            var firebaseBookingRequests = (await firebase
                .Child("bookingRequests")
                .OnceAsync<BookingRequest>())
                .Select(item => new BookingRequest
                {
                    Key = item.Key,
                    RoomId = item.Object.RoomId,
                    UserId = item.Object.UserId,
                    Status = item.Object.Status,
                    Timestamp = item.Object.Timestamp
                })
                .ToList();

            BookingRequests.Clear();
            foreach (var request in firebaseBookingRequests)
            {
                BookingRequests.Add(request);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }

    }


    private async void OnAcceptButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var bookingRequest = button?.CommandParameter as BookingRequest;

        if (bookingRequest != null)
        {
            try
            {
                

                await firebase
                    .Child("bookingRequests")
                    .Child(bookingRequest.Key)
                    .PatchAsync(new { Status = "Approved" });

                // Notifying the student if they are approved
                await NotifyStudent(bookingRequest.UserId, "Your booking request has been approved!");

                await DisplayAlert("Success", "Booking request approved!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

    private async void OnRemoveButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var bookingRequest = button?.CommandParameter as BookingRequest;

        if (bookingRequest != null)
        {
            try
            {
               

                await firebase
                    .Child("bookingRequests")
                    .Child(bookingRequest.Key)
                    .PatchAsync(new { Status = "Declined" });

                // Notifying the student if they are declined
                await NotifyStudent(bookingRequest.UserId, "Your booking request has been declined.");

                await DisplayAlert("Success", "Booking request declined!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

    private async Task NotifyStudent(string userId, string message)
    {
        

        var notification = new Notification
        {
            Message = message,
            Timestamp = DateTime.UtcNow,
            IsRead = false
        };

        await firebase
            .Child("notifications")
            .Child(userId)
            .PostAsync(notification);
    }

    private void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var bookingRequest = button?.CommandParameter as BookingRequest;

        if (bookingRequest != null)
        {
           
           
            DisplayAlert("Success", "Booking request saved!", "OK");
        }
    }
}