using System.Globalization;

namespace RoomScout.Converters
{
    public class AmenityToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string amenity)
            {
                return amenity.ToLower() switch
                {
                    "wifi" => "wifi.png",
                    "free electricity" => "electricity.png",
                    "bed" => "bed.png",
                    "washing machine" => "washing.png",
                    "study table" => "studytable.png",
                    "showers" => "showers.png",
                    "chair" => "chair.png",
                    "parking" => "parking.png",
                    "sink" => "sink.png",
                    "shared kitchen" => "kitchen.png",
                    "kitchenette" => "kitchenette.png",
                    "security" => "security.png",
                    "backupwater" => "water.png",
                    "backupelectricty" => "backup_power.png",
                    "ownelectricity" => "electricity_meter.png",
                    // Negative amenities (with 'no' prefix)
                    "noshowers" => "no_shower.png",
                    "paywifi" => "paid_wifi.png",
                    "nochair" => "no_chair.png",
                    "nostudytable" => "no_desk.png",
                    "bringownbed" => "no_bed.png",
                    "nohotwater" => "no_hot_water.png",
                    "noelectricfence" => "no_fence.png",
                    _ => "amenity.png", // Default icon for unrecognized amenities
                };
            }
            return "amenity.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}