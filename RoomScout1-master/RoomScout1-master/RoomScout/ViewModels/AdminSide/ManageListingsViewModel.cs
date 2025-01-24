using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace RoomScout.ViewModels.AdminSide
{
    public class ManageListingsViewModel : INotifyPropertyChanged
    {
        private bool _isBachelorFrameVisible;
        private bool _isSingleFrameVisible;
        private bool _isSharingFrameVisible;

        public ICommand ToggleBachelorFrameCommand { get; }
        public ICommand ToggleSingleFrameCommand { get; }
        public ICommand ToggleSharingFrameCommand { get; }
        public ICommand NavigateToAddNewListingPageCommand { get; }

        public ManageListingsViewModel()
        {
            ToggleBachelorFrameCommand = new Command(() => ToggleFrameVisibility(ref _isBachelorFrameVisible, nameof(IsBachelorFrameVisible)));
            ToggleSingleFrameCommand = new Command(() => ToggleFrameVisibility(ref _isSingleFrameVisible, nameof(IsSingleFrameVisible)));
            ToggleSharingFrameCommand = new Command(() => ToggleFrameVisibility(ref _isSharingFrameVisible, nameof(IsSharingFrameVisible)));
            NavigateToAddNewListingPageCommand = new Command(OnNavigateToAddNewListingPage);
        }

        private async void OnNavigateToAddNewListingPage()
        {
            // Navigate to the "AddNewListingPage"
            await Shell.Current.GoToAsync("AddListingPage");
        }

        public bool IsBachelorFrameVisible
        {
            get => _isBachelorFrameVisible;
            set
            {
                if (_isBachelorFrameVisible != value)
                {
                    _isBachelorFrameVisible = value;
                    OnPropertyChanged(nameof(IsBachelorFrameVisible));
                }
            }
        }

        public bool IsSingleFrameVisible
        {
            get => _isSingleFrameVisible;
            set
            {
                if (_isSingleFrameVisible != value)
                {
                    _isSingleFrameVisible = value;
                    OnPropertyChanged(nameof(IsSingleFrameVisible));
                }
            }
        }

        public bool IsSharingFrameVisible
        {
            get => _isSharingFrameVisible;
            set
            {
                if (_isSharingFrameVisible != value)
                {
                    _isSharingFrameVisible = value;
                    OnPropertyChanged(nameof(IsSharingFrameVisible));
                }
            }
        }

        private void ToggleFrameVisibility(ref bool frameVisibility, string propertyName)
        {
            frameVisibility = !frameVisibility;
            OnPropertyChanged(propertyName);
        }

        // INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}