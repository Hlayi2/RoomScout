using System.Collections.ObjectModel;
using RoomScout.Models.AdminSide;
using RoomScout.ViewModels.AdminSide;

namespace RoomScout.Views.AdminSide;

public partial class BookingRequestsPage : ContentPage
{

    public ObservableCollection<BookingRequest> BookingRequests { get; set; }
    public BookingRequestsPage()
    {
        InitializeComponent();
        MessagingCenter.Subscribe<BookingRequestsViewModel, string>(this, "ShowAlert", (sender, message) =>
        {
            DisplayAlert("Error", message, "OK");
        });
        BookingRequests = new ObservableCollection<BookingRequest>
            {
                // Add sample data or bind to your actual data source
                new BookingRequest { Name = "John Doe", ProfilePicture = "male.png" },
                new BookingRequest { Name = "Jane Smith", ProfilePicture = "female.png" },
                new BookingRequest { Name = "Chantelle Mathye", ProfilePicture = "female.png" }
            };
        BindingContext = this;
    }


    private void OnAcceptButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var bookingRequest = button?.CommandParameter as BookingRequest;

        if (bookingRequest != null)
        {
            bookingRequest.IsDateTimeVisible = true;
            bookingRequest.IsImageVisible = false; // Hide the image
        }
    }

    private void OnRemoveButtonClicked(object sender, EventArgs e)
    {
        // Remove the frame or update the data source
        var button = sender as Button;
        var frame = button?.Parent?.Parent?.Parent as Frame;

        if (frame != null)
        {
            frame.IsVisible = false;
        }
    }

    private void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var bookingRequest = button?.CommandParameter as BookingRequest;

        if (bookingRequest != null)
        {
            // Check if Date and Time are valid
            if (bookingRequest.Date != default && bookingRequest.Time != default)
            {
                // Format the date and time for the confirmation message
                string formattedDate = bookingRequest.Date.ToString("yyyy-MM-dd");
                string formattedTime = bookingRequest.Time.ToString(@"hh\:mm");

                // Include additional information in the confirmation message
                bookingRequest.ConfirmationMessage = $"Booking confirmed for {formattedDate} at {formattedTime}. " +
                                                    $"Additional Information: {bookingRequest.AdditionalInformation}";
                bookingRequest.IsDateTimeVisible = false;
                bookingRequest.AreButtonsVisible = false;
            }
            else
            {
                // Use DisplayAlert directly
                DisplayAlert("Error", "Please select a date and time.", "OK");
            }
        }
    }


}