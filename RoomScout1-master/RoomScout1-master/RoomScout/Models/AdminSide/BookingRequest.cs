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
        private string _date;
        private string _time;
        private string _confirmationMessage;

        public string Name { get; set; }
        public string ProfilePicture { get; set; }

        public bool IsDateTimeVisible
        {
            get => _isDateTimeVisible;
            set
            {
                _isDateTimeVisible = value;
                OnPropertyChanged();
            }
        }

        public string Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged();
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