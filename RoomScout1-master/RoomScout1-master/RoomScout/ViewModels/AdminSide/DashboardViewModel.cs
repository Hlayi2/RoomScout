using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoomScout.ViewModels.AdminSide
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private bool _isComplaintFormVisible;
        private string _complaintText;

        public ICommand ShowComplaintFormCommand { get; }
        public ICommand HideComplaintFormCommand { get; }
        public ICommand SendComplaintCommand { get; }

        public DashboardViewModel()
        {
            _isComplaintFormVisible = false;

            ShowComplaintFormCommand = new Command(() => IsComplaintFormVisible = true);
            HideComplaintFormCommand = new Command(() => IsComplaintFormVisible = false);
            SendComplaintCommand = new Command(OnSendComplaint);
        }

        public bool IsComplaintFormVisible
        {
            get => _isComplaintFormVisible;
            set
            {
                if (_isComplaintFormVisible != value)
                {
                    _isComplaintFormVisible = value;
                    OnPropertyChanged(nameof(IsComplaintFormVisible));
                }
            }
        }

        public string ComplaintText
        {
            get => _complaintText;
            set
            {
                if (_complaintText != value)
                {
                    _complaintText = value;
                    OnPropertyChanged(nameof(ComplaintText));
                }
            }
        }

        private async void OnSendComplaint()
        {
            if (string.IsNullOrWhiteSpace(ComplaintText))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter your complaint.", "OK");
                return;
            }

            // Simulate sending the complaint (e.g., API call or database save)
            await Task.Delay(1000); // Simulate a delay

            // Show confirmation message
            await Application.Current.MainPage.DisplayAlert("Success", "Your complaint has been sent.", "OK");

            // Clear the complaint text and hide the form
            ComplaintText = string.Empty;
            IsComplaintFormVisible = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

