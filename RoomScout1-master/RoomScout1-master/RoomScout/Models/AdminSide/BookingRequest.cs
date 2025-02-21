using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RoomScout.Models.AdminSide
{
    public class BookingRequest : INotifyPropertyChanged
    {
        private string _status;
        private string _confirmationMessage;
        private Color _statusColor;
        private bool _isDateTimeVisible;
        private string _additionalInformation;
        private string _landlordFeedback;

        public string Key { get; set; }
        public string RoomId { get; set; }
        
        public string UserId { get; set; }

        public string LandlordFeedback
        {
            get => _landlordFeedback;
            set
            {
                _landlordFeedback = value;
                OnPropertyChanged();
                // Update confirmation message when feedback changes
                UpdateConfirmationMessage();
            }
        }

        private void UpdateConfirmationMessage()
        {
            if (!string.IsNullOrEmpty(LandlordFeedback))
            {
                ConfirmationMessage = Status switch
                {
                    "Approved" => $"Booking approved with message: {LandlordFeedback}",
                    "Declined" => $"Booking declined with reason: {LandlordFeedback}",
                    _ => LandlordFeedback
                };
            }
            else
            {
                ConfirmationMessage = Status switch
                {
                    "Approved" => "Booking request approved",
                    "Declined" => "Booking request declined",
                    _ => "Pending approval"
                };
            }
        }
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string ConfirmationMessage
        {
            get => _confirmationMessage;
            set
            {
                _confirmationMessage = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Color StatusColor
        {
            get => _statusColor;
            set
            {
                _statusColor = value;
                OnPropertyChanged();
            }
        }
        public bool IsDateTimeVisible
        {
            get => _isDateTimeVisible;
            set
            {
                _isDateTimeVisible = value;
                OnPropertyChanged();
            }
        }
        public string AdditionalInformation
        {
            get => _additionalInformation;
            set
            {
                _additionalInformation = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}