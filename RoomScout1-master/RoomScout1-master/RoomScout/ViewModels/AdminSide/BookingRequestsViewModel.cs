using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RoomScout.Models.AdminSide;

namespace RoomScout.ViewModels.AdminSide
{
    public class BookingRequestsViewModel : BaseViewModel 
    {
        public ObservableCollection<BookingRequest> BookingRequests { get; set; }
        public ICommand AcceptCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand SaveCommand { get; }

        public BookingRequestsViewModel()
        {
            BookingRequests = new ObservableCollection<BookingRequest>
        {
            new BookingRequest { Name = "John Doe", ProfilePicture = "male.png" },
            new BookingRequest { Name = "Jane Smith", ProfilePicture = "female.png" },
            new BookingRequest { Name = "Chris Johnson", ProfilePicture = "female.png" }
        };

            AcceptCommand = new Command<BookingRequest>(OnAccept);
            RemoveCommand = new Command<BookingRequest>(OnRemove);
            SaveCommand = new Command<BookingRequest>(OnSave);
        }

        private void OnAccept(BookingRequest request)
        {
            request.IsDateTimeVisible = true;
        }

        private void OnRemove(BookingRequest request)
        {
            BookingRequests.Remove(request);
        }

        private void OnSave(BookingRequest request)
        {
            if (request.Date != default && request.Time != default)
            {
                // Format the date and time for the confirmation message
                string formattedDate = request.Date.ToString("yyyy-MM-dd");
                string formattedTime = request.Time.ToString(@"hh\:mm");
                request.ConfirmationMessage = $"Booking confirmed for {formattedDate} at {formattedTime}. " +
                                              $"Additional Information: {request.AdditionalInformation}";
                request.IsDateTimeVisible = false;
            }
            else
            {
                // Send a message to display an alert
                MessagingCenter.Send(this, "ShowAlert", "Please select a date and time.");
            }
        }

    }
}
