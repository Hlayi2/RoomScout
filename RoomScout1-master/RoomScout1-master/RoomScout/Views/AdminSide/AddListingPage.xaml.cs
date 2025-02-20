using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using RoomScout.Models.AdminSide;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RoomScout.Models.Location;


namespace RoomScout.Views.AdminSide
{
    public partial class AddListingPage : ContentPage
    {
        private static readonly FirebaseClient firebase = new FirebaseClient("https://roomscout-a194c-default-rtdb.firebaseio.com/");
        public ObservableCollection<Rule> Rules { get; set; }
        private Pin currentPin;
        public ObservableCollection<string> ImageBase64List { get; set; } = new();
        private const int MaxImages = 5;

        public AddListingPage()
        {
            InitializeComponent();
            Rules = new ObservableCollection<Rule>();
            RulesList.ItemsSource = Rules;

            // Initialize map to South Africa's coordinates
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var southAfricaLocation = new Location(-30.5595, 22.9375);
                var initialMapSpan = MapSpan.FromCenterAndRadius(
                    southAfricaLocation,
                    Distance.FromKilometers(1000));
                locationMap.MoveToRegion(initialMapSpan);
            });
        }

        private async void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            try
            {
                // Remove existing pin if any
                if (currentPin != null)
                    locationMap.Pins.Remove(currentPin);

                // Create new pin at clicked location
                currentPin = new Pin
                {
                    Label = "Property Location",
                    Location = e.Location,
                    Type = PinType.Generic
                };

                locationMap.Pins.Add(currentPin);

                // Update coordinates entry
                CoordinatesEntry.Text = $"{e.Location.Latitude:F6}, {e.Location.Longitude:F6}";

                // Try to get address for the location
                try
                {
                    var placemarks = await Geocoding.Default.GetPlacemarksAsync(e.Location.Latitude, e.Location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)
                    {
                        StreetEntry.Text = placemark.Thoroughfare;
                        SuburbEntry.Text = placemark.SubLocality;
                        CityEntry.Text = placemark.Locality;
                        PostalCodeEntry.Text = placemark.PostalCode;
                    }
                }
                catch
                {
                    await DisplayAlert("Note", "Location selected but unable to get address details", "OK");
                }

                // Move map to clicked location with appropriate zoom
                var mapSpan = MapSpan.FromCenterAndRadius(
                    e.Location,
                    Distance.FromKilometers(1));
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    locationMap.MoveToRegion(mapSpan);
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to set location: {ex.Message}", "OK");
            }
        }

    

        private void OnPriceTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                return;

            // Only allow numbers and decimal point
            string newText = new string(e.NewTextValue.Where(c => char.IsDigit(c) || c == '.').ToArray());

            // Ensure only one decimal point
            if (newText.Count(c => c == '.') > 1)
            {
                newText = newText.Replace(".", "");
                if (newText.Length > 0)
                    newText = newText.Insert(newText.Length - 2, ".");
            }

            // Format to 2 decimal places if there's a decimal point
            if (newText.Contains('.'))
            {
                string[] parts = newText.Split('.');
                if (parts[1].Length > 2)
                    newText = parts[0] + "." + parts[1].Substring(0, 2);
            }

            // Update the entry only if the text has changed
            if (newText != e.NewTextValue)
            {
                ((Entry)sender).Text = newText;
            }
        }

        private void OnAddRuleClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RuleEntry.Text))
            {
                DisplayAlert("Error", "Please enter a rule", "OK");
                return;
            }

            Rules.Add(new Rule
            {
                Number = $"{Rules.Count + 1}.",
                Text = RuleEntry.Text
            });

            RuleEntry.Text = string.Empty;
        }

        private void OnDeleteRuleClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Rule ruleToDelete)
            {
                Rules.Remove(ruleToDelete);
                UpdateRuleNumbers();
            }
        }

        private void UpdateRuleNumbers()
        {
            int number = 1;
            foreach (var rule in Rules)
            {
                rule.Number = $"{number}.";
                number++;
            }
        }

        private async void OnUploadClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickMultipleAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Select up to 5 images"
                });

                if (result?.Any() ?? false)
                {
                    if (result.Count() > MaxImages)
                    {
                        await DisplayAlert("Error", $"Maximum {MaxImages} images allowed", "OK");
                        return;
                    }

                    ImageBase64List.Clear();
                    foreach (var file in result)
                    {
                        using var stream = await file.OpenReadAsync();
                        byte[] bytes = new byte[stream.Length];
                        await stream.ReadAsync(bytes);
                        ImageBase64List.Add(Convert.ToBase64String(bytes));
                    }

                    FileNameLabel.Text = result.Count() == 1
                        ? Path.GetFileName(result.First().FullPath)
                        : $"{result.Count()} files selected";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(CoordinatesEntry.Text))
            {
                await DisplayAlert("Error", "Please select a location on the map", "OK");
                return;
            }


            if (RoomTypePicker.SelectedIndex <= 0)
            {
                await DisplayAlert("Error", "Please select a room type", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(PriceEntry.Text))
            {
                await DisplayAlert("Error", "Please enter a price", "OK");
                return;
            }




            var listingData = new Listing // Use your Listing model
            {
                Address = new LocationData // Create LocationData class if missing
                {
                    Coordinates = CoordinatesEntry.Text,
                    Street = StreetEntry.Text,
                    Suburb = SuburbEntry.Text,
                    City = CityEntry.Text,
                    PostalCode = PostalCodeEntry.Text
                },
                Contact = new ContactData // Create ContactData class if missing
                {
                    AlternativePhone = AlternativePhoneEntry.Text
                },
                RoomType = RoomTypePicker.SelectedItem?.ToString(),
                Price = decimal.Parse(PriceEntry.Text),
                Amenities = new AmenitiesData // Create AmenitiesData class if missing
                {
                    Wifi = WifiCheck.IsChecked,
                    FreeElectricity = FreeElectricityCheck.IsChecked,
                    Bed = BedCheck.IsChecked,
                    WashingMachine = WashingMachineCheck.IsChecked,
                    StudyTable = StudyTableCheck.IsChecked,
                    Showers = ShowersCheck.IsChecked
                },
                Rules = Rules.ToList(),
                Images = ImageBase64List.ToList(),
                DateAdded = DateTime.UtcNow
            };
            try
            {
                // Push to Firebase
                var response = await firebase
                    .Child("listings")
                    .PostAsync(listingData);

                await DisplayAlert("Success", "Listing saved!", "OK");
                await Navigation.PushAsync(new DashboardProfile());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}

public class Rule : INotifyPropertyChanged
{
    private string number;
    private string text;

    public string Number
    {
        get => number;
        set
        {
            if (number != value)
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
    }

    public string Text
    {
        get => text;
        set
        {
            if (text != value)
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
