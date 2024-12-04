using RoomScout.Models.AdminSide;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Storage;

namespace RoomScout.ViewModels.AdminSide
{
    public class LandlordDashboardViewModel : INotifyPropertyChanged
    {
        private Landlord landlord;

        public Landlord Landlord
        {
            get => landlord;
            set
            {
                SetProperty(ref landlord, value);
                SaveLandlordData(); // Save data whenever the property changes
            }
        }

        public ICommand AddListingCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LandlordDashboardViewModel()
        {
            LoadLandlordData(); // Load data when the ViewModel is initialized
        }

        private void LoadLandlordData()
        {
            Landlord = new Landlord
            {
                FullNames = Preferences.Get(nameof(Landlord.FullNames), "John Doe"),
                Email = Preferences.Get(nameof(Landlord.Email), string.Empty),
                ContactDetails = Preferences.Get(nameof(Landlord.ContactDetails), string.Empty),
                IdPassport = Preferences.Get(nameof(Landlord.IdPassport), string.Empty),
                AccommodationName = Preferences.Get(nameof(Landlord.AccommodationName), string.Empty),
                Address = Preferences.Get(nameof(Landlord.Address), string.Empty),
                Location = Preferences.Get(nameof(Landlord.Location), string.Empty),
                ProfilePicture = Preferences.Get(nameof(Landlord.ProfilePicture), "profiles.png")
            };
            Console.WriteLine($"Loaded ProfilePicture: {Landlord.ProfilePicture}");
        }

        private void SaveLandlordData()
        {
            Preferences.Set(nameof(Landlord.FullNames), Landlord.FullNames);
            Preferences.Set(nameof(Landlord.Email), Landlord.Email);
            Preferences.Set(nameof(Landlord.ContactDetails), Landlord.ContactDetails);
            Preferences.Set(nameof(Landlord.IdPassport), Landlord.IdPassport);
            Preferences.Set(nameof(Landlord.AccommodationName), Landlord.AccommodationName);
            Preferences.Set(nameof(Landlord.Address), Landlord.Address);
            Preferences.Set(nameof(Landlord.Location), Landlord.Location);
            Preferences.Set(nameof(Landlord.ProfilePicture), Landlord.ProfilePicture);

            Console.WriteLine($"Saved ProfilePicture: {Landlord.ProfilePicture}");
        }

        protected void SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(backingStore, value)) return;

            backingStore = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
    }
}
