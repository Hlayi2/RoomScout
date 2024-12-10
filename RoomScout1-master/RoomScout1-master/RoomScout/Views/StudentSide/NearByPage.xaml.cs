using Microsoft.Maui.Controls.Maps;
using RoomScout.ViewModels.StudentSide;

namespace RoomScout.Views.StudentSide
{
    public partial class NearByPage : ContentPage
    {
        private NearByViewModel ViewModel => (NearByViewModel)BindingContext;

        public NearByPage()
        {
            InitializeComponent();
            BindingContext = new NearByViewModel();

            // Event handler for map pins
            map.MapClicked += OnMapPinClicked;  // Changed from PinClicked to MapClicked
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel != null)
            {
                ViewModel.InitializeMap(map);
            }
        }

        private async void OnMapPinClicked(object sender, MapClickedEventArgs e)  // Changed to MapClickedEventArgs
        {
            if (sender is Pin pin)
            {
                var pinData = pin.BindingContext as dynamic;
                if (pinData != null)
                {
                    string details = $"Address: {pin.Address}\n\n" +
                                   "Features:\n" +
                                   "- Furnished\n" +
                                   "- Water Included\n" +
                                   "- WiFi Available\n" +
                                   "- Security\n\n" +
                                   "Would you like to contact the landlord?";

                    bool answer = await DisplayAlert(pin.Label, details, "Call Landlord", "Cancel");
                    if (answer)
                    {
                        try
                        {
                            PhoneDialer.Default.Open(pinData.Phone);
                        }
                        catch (Exception)
                        {
                            await DisplayAlert("Error", "Unable to make phone call", "OK");
                        }
                    }
                }
            }
        }
    }
}