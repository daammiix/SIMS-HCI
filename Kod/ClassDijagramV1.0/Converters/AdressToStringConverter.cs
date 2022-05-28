using ClassDijagramV1._0.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ClassDijagramV1._0.Converters
{
    public class AdressToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value == "")
            {
                return "";
            }
            var adress = (Address)value;
            return adress.StreetName + " " + adress.StreetNumber + ", " + adress.City;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
