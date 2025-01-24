using System.Collections.ObjectModel;
using RoomScout.Models.AdminSide;

namespace RoomScout.Views.AdminSide;

public partial class BookingRequestsPage : ContentPage
{

    public ObservableCollection<BookingRequest> BookingRequests { get; set; }
    public BookingRequestsPage()
    {
        InitializeComponent();
        BookingRequests = new ObservableCollection<BookingRequest>
            {
                // Add sample data or bind to your actual data source
                new BookingRequest { Name = "John Doe", ProfilePicture = "profiles.png" },
                new BookingRequest { Name = "Jane Smith", ProfilePicture = "profiles.png" },
                new BookingRequest { Name = "Chantelle Mathye", ProfilePicture = "profiles.png" }
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
            if (!string.IsNullOrEmpty(bookingRequest.Date) && !string.IsNullOrEmpty(bookingRequest.Time))
            {
                bookingRequest.ConfirmationMessage = $"Booking confirmed for {bookingRequest.Date} at {bookingRequest.Time}.";
                bookingRequest.IsDateTimeVisible = false;
            }
            else
            {
                DisplayAlert("Error", "Please enter both date and time.", "OK");
            }
        }
    }

   
}