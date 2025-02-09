using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace RoomScout.Converters
{
    public class ReadStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isRead)
            {
                return isRead ? Color.FromArgb("#EEEEEE") : Color.FromArgb("#E2F1E7"); // Light gray for read, light green for unread
            }
            return Color.FromArgb("#E2F1E7"); // Default to unread color
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}