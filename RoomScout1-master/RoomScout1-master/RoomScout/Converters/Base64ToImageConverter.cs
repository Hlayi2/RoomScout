// RoomScout/Converters/Base64ToImageConverter.cs
using System.Globalization;

namespace RoomScout.Converters
{
    public class Base64ToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string base64String)
            {
                byte[] imageBytes = System.Convert.FromBase64String(base64String);
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}