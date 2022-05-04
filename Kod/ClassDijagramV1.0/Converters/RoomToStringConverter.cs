using Model;
using System;
using System.Windows.Data;

namespace ClassDijagramV1._0.Converters
{
    public class RoomToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value == "")
            {
                return "";
            }
            var room = (Room)value;
            return room.RoomID + " " + room.RoomName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
