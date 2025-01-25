using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RoomScout.Models.AdminSide
{
    public class BookingRequest : INotifyPropertyChanged
    {
        private bool _isDateTimeVisible;
        private string _confirmationMessage;

        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string AdditionalInformation { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public bool IsDateTimeVisible
        {
            get => _isDateTimeVisible;
            set
            {
                _isDateTimeVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isImageVisible = true;
        public bool IsImageVisible
        {
            get => _isImageVisible;
            set
            {
                _isImageVisible = value;
                OnPropertyChanged(nameof(IsImageVisible));
            }
        }

        private bool _areButtonsVisible = true;
        public bool AreButtonsVisible
        {
            get => _areButtonsVisible;
            set
            {
                _areButtonsVisible = value;
                OnPropertyChanged(nameof(AreButtonsVisible));
            }
        }

        public string ConfirmationMessage
        {
            get => _confirmationMessage;
            set
            {
                _confirmationMessage = value;
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