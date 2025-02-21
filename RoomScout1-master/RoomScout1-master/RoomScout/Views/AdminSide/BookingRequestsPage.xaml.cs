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
                    Timestamp = item.Object.Timestamp,
                    LandlordFeedback = item.Object.LandlordFeedback, // Make sure this is included
                    Name = item.Object.Name,
                    ProfilePicture = item.Object.ProfilePicture,
                    Date = item.Object.Date,
                    Time = item.Object.Time,
                    ConfirmationMessage = GetConfirmationMessage(item.Object), // Add this helper method
                    StatusColor = GetStatusColor(item.Object.Status)
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
    private string GetConfirmationMessage(BookingRequest request)
    {
        if (!string.IsNullOrEmpty(request.LandlordFeedback))
        {
            switch (request.Status)
            {
                case "Approved":
                    return $"Approved - {request.LandlordFeedback}";
                case "Declined":
                    return $"Declined - {request.LandlordFeedback}";
                default:
                    return request.LandlordFeedback;
            }
        }
        return request.Status;
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


    private async void OnAcceptButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var bookingRequest = button?.CommandParameter as BookingRequest;

        if (bookingRequest != null)
        {
            try
            {


                // Showing feedback prompt
                string feedback = await DisplayPromptAsync(
                    "Accept Booking",
                "Add a message for the student (optional):",
                "Send",
                "Cancel",
                "Enter your message here",
                -1);

                // If user didn't cancel
                if (feedback != null)
                {
                    await firebase
                        .Child("bookingRequests")
                        .Child(bookingRequest.Key)
                        .PatchAsync(new
                        {
                            Status = "Approved",
                            LandlordFeedback = feedback,
                             LastUpdated= DateTime.UtcNow
                        });

                    // Notifying the student with the feedback
                    bookingRequest.Status = "Approved";
                    bookingRequest.LandlordFeedback = feedback;
                    bookingRequest.ConfirmationMessage = GetConfirmationMessage(bookingRequest);
                    bookingRequest.StatusColor = GetStatusColor("Approved");

                    // Send notification
                    string notificationMessage = string.IsNullOrEmpty(feedback)
                        ? "Your booking request has been approved!"
                        : $"Your booking request has been approved! Message: {feedback}";

                    await NotifyStudent(bookingRequest.UserId, notificationMessage);

                    // Refresh the list to ensure UI is updated
                    LoadBookingRequests();

                    await DisplayAlert("Success", "Booking request approved!", "OK");
                }
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

                // Show feedback prompt
                string feedback = await DisplayPromptAsync(
                    "Decline Booking",
                    "Please provide a reason (optional):",
                    "Send",
                    "Cancel",
                    "Enter your reason here",
                    -1);
                // If user didn't cancel
                if (feedback != null)
                {
                    await firebase
                        .Child("bookingRequests")
                        .Child(bookingRequest.Key)
                        .PatchAsync(new


                        {
                            Status = "Declined",
                            LandlordFeedback = feedback,
                            LastUpdated = DateTime.UtcNow
                        });

                    // Update local object
                    bookingRequest.Status = "Declined";
                    bookingRequest.LandlordFeedback = feedback;
                    bookingRequest.ConfirmationMessage = GetConfirmationMessage(bookingRequest);
                    bookingRequest.StatusColor = GetStatusColor("Declined");

                    // Notifying the student with the feedback
                    string notificationMessage = string.IsNullOrEmpty(feedback)
                        ? "Your booking request has been declined."
                        : $"Your booking request has been declined. Reason: {feedback}";

                    await NotifyStudent(bookingRequest.UserId, notificationMessage);

                    LoadBookingRequests();

                    await DisplayAlert("Success", "Booking request declined!", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

    private async Task NotifyStudent(string userId, string message)
    {
        try
        {
            var notification = new Notification
            {
                Message = message,
                Timestamp = DateTime.UtcNow,
                IsRead = false
            };

            // Send to notifications node
            var notificationResult = await firebase
                .Child("notifications")
                .Child(userId)
                .PostAsync(notification);

            Console.WriteLine($"Notification sent successfully to {userId}");

            // Optional: Update the booking request with notification status
            if (notificationResult != null)
            {
                await firebase
                    .Child("bookingRequests")
                    .Child(notificationResult.Key)
                    .Child("notificationSent")
                    .PutAsync(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending notification: {ex.Message}");
            // Create a retry mechanism
            await RetryNotification(userId, message);
        }


    }

    private async Task RetryNotification(string userId, string message, int maxAttempts = 3)
    {
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                await Task.Delay(attempt * 1000); // Exponential backoff
                var notification = new Notification
                {
                    Message = message,
                    Timestamp = DateTime.UtcNow,
                    IsRead = false,
                    Type = "booking_update_retry"
                };

                await firebase
                    .Child("notifications")
                    .Child(userId)
                    .PostAsync(notification);

                Console.WriteLine($"Notification sent successfully on retry attempt {attempt}");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Retry attempt {attempt} failed: {ex.Message}");
                if (attempt == maxAttempts)
                {
                    // Log failed notification
                    await firebase
                        .Child("failed_notifications")
                        .PostAsync(new
                        {
                            UserId = userId,
                            Message = message,
                            Timestamp = DateTime.UtcNow,
                            Attempts = attempt
                        });
                }
            }
        }
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