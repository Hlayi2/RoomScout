using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoomScout.ViewModels.AdminSide
{
   public class ManageListingsViewModel : INotifyPropertyChanged
    {
        private bool _isBachelorFrameVisible;
        private bool _isSingleFrameVisible;
        private bool _isSharingFrameVisible;

        public ManageListingsViewModel()
        {
            ToggleBachelorFrameCommand = new Command(() => ToggleFrameVisibility(ref _isBachelorFrameVisible, nameof(IsBachelorFrameVisible)));
            ToggleSingleFrameCommand = new Command(() => ToggleFrameVisibility(ref _isSingleFrameVisible, nameof(IsSingleFrameVisible)));
            ToggleSharingFrameCommand = new Command(() => ToggleFrameVisibility(ref _isSharingFrameVisible, nameof(IsSharingFrameVisible)));
        }

        public bool IsBachelorFrameVisible
        {
            get => _isBachelorFrameVisible;
            set
            {
                _isBachelorFrameVisible = value;
                OnPropertyChanged(nameof(IsBachelorFrameVisible));
            }
        }

        public bool IsSingleFrameVisible
        {
            get => _isSingleFrameVisible;
            set
            {
                _isSingleFrameVisible = value;
                OnPropertyChanged(nameof(IsSingleFrameVisible));
            }
        }

        public bool IsSharingFrameVisible
        {
            get => _isSharingFrameVisible;
            set
            {
                _isSharingFrameVisible = value;
                OnPropertyChanged(nameof(IsSharingFrameVisible));
            }
        }

        public ICommand ToggleBachelorFrameCommand { get; }
        public ICommand ToggleSingleFrameCommand { get; }
        public ICommand ToggleSharingFrameCommand { get; }

        private void ToggleFrameVisibility(ref bool frameVisibility, string propertyName)
        {
            frameVisibility = !frameVisibility;
            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
