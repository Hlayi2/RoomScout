using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Windows.Input;

namespace RoomScout.ViewModels.StudentSide
{
    public class NearByViewModel : BaseViewModel
    {
        private Microsoft.Maui.Controls.Maps.Map map;
        public ICommand PinClickedCommand { get; }

        public NearByViewModel()
        {
            PinClickedCommand = new Command<Pin>(OnPinClicked);
        }

        public void InitializeMap(Microsoft.Maui.Controls.Maps.Map map)
        {
            this.map = map;
            SetupMap();
            AddPins();
        }

        private void SetupMap()
        {
            // Center on University of Limpopo
            var ulLocation = new Location(-23.8796, 29.7390);
            var mapSpan = MapSpan.FromCenterAndRadius(
                ulLocation,
                Distance.FromKilometers(2));

            map.MoveToRegion(mapSpan);
            map.MapType = MapType.Satellite;
        }

        private void AddPins()
        {
            // Add University pin
            var ulLocation = new Location(-23.8796, 29.7390);
            AddPin("University of Limpopo", ulLocation, "Main Campus");

            // Add Sample Bachelor Rooms
            AddRentalPin("Bachelor Room", -23.8750, 29.7395, "R3500/month",
                "123 University Road", RoomType.Bachelor,
                "John Smith", "0712345678");

            AddRentalPin("Bachelor Suite", -23.8760, 29.7385, "R3800/month",
                "456 Campus Street", RoomType.Bachelor,
                "Sarah Johnson", "0723456789");

            // Add Sample Single Rooms
            AddRentalPin("Single Room A", -23.8780, 29.7380, "R2500/month",
                "789 Student Avenue", RoomType.Single,
                "David Brown", "0734567890");

            AddRentalPin("Single Room B", -23.8790, 29.7375, "R2700/month",
                "101 College Road", RoomType.Single,
                "Mary Wilson", "0745678901");

            // Add Sample Sharing Rooms
            AddRentalPin("Shared Room 1", -23.8810, 29.7400, "R1800/month",
                "202 Academic Drive", RoomType.Sharing,
                "Peter Parker", "0756789012");

            AddRentalPin("Shared Room 2", -23.8820, 29.7395, "R1900/month",
                "303 Campus View", RoomType.Sharing,
                "James Lee", "0767890123");
        }

        private void AddPin(string label, Location location, string address)
        {
            var pin = new Pin
            {
                Label = label,
                Location = location,
                Address = address,
                Type = PinType.Generic
            };
            map.Pins.Add(pin);
        }

        private void AddRentalPin(string title, double latitude, double longitude,
            string price, string address, RoomType type,
            string landlordName, string landlordPhone)
        {
            var pin = new Pin
            {
                Label = $"{title} ({type})",
                Location = new Location(latitude, longitude),
                Address = $"{price}\n{address}\nLandlord: {landlordName}",
                Type = PinType.Place,
                BindingContext = new { Phone = landlordPhone, Name = landlordName }
            };
            map.Pins.Add(pin);
        }

        private async void OnPinClicked(Pin pin)
        {
            if (pin == null) return;

            var pinData = pin.BindingContext as dynamic;
            if (pinData == null) return;

            string details = $"Address: {pin.Address}\n\n" +
                           "Features:\n" +
                           "- Furnished\n" +
                           "- Water Included\n" +
                           "- WiFi Available\n" +
                           "- Security\n\n" +
                           "Would you like to contact the landlord?";

            bool response = await Application.Current.MainPage.DisplayAlert(
                pin.Label, details, "Call Landlord", "Cancel");

            if (response)
            {
                try
                {
                    PhoneDialer.Default.Open(pinData.Phone);
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error", "Unable to make phone call", "OK");
                }
            }
        }
    }

    public enum RoomType
    {
        Bachelor,
        Single,
        Sharing
    }
}