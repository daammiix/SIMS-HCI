using Model;
using System;
using System.Windows.Data;

namespace ClassDijagramV1._0.Converters
{
    public class RoomToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Room room = (Room)value;
            if (room != null)
            {
                return room.RoomID + " " + room.RoomName;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
