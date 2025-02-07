using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RoomScout.Models.AdminSide;

namespace RoomScout.ViewModels.AdminSide
{
    public class NotificationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Notification> Notifications { get; set; }

        public NotificationViewModel()
        {
            // Initialize notifications
            Notifications = new ObservableCollection<Notification>
            {
                new Notification { Title = "Welcome to RoomScout, Landlord.", Description = "We're excited to have you on board! Start by setting up your profile. Happy Hunting.", IsRead = false, Icon="icon.png" },
                new Notification { Title = "Finish setting up your profile.", Description = "Complete your profile to start listing propertie and to make things easier for your future tenants.", IsRead = false, Icon="icon.png" },
                new Notification { Title = "Upload required documents.", Description = "Upload your ID and proof of address to verify your account. Know that if your required documents do not meet our criteria they will be rejected.", IsRead = false, Icon="icon.png" },
                new Notification { Title = "Upgrade to premium.", Description = "Get access to premium features like priority support and direct control of your listings.", IsRead = false, Icon="icon.png" },
                new Notification { Title = "You have a booking request from John Doe.", Description = "John is requesting to view your accomodation!", IsRead = false, Icon="icon.png" },
                new Notification { Title = "You have a booking request from Jane Smith.", Description = "Jane is requesting to view your accomodation!", IsRead = false, Icon="icon.png" },
                new Notification { Title = "You have a booking request from Alice Johnson.", Description = "Alice is requesting to view your accomodation", IsRead = false, Icon="icon.png" }
            };

            // Initialize the command
            ToggleNotificationCommand = new Command<Notification>(ToggleNotification);
        }

        public ICommand ToggleNotificationCommand { get; }

        private void ToggleNotification(Notification notification)
        {
            if (notification == null) return;

            // Toggle expanded state
            notification.IsExpanded = !notification.IsExpanded;

            // Mark as read if expanded
            if (notification.IsExpanded)
            {
                notification.IsRead = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}