using ClassDijagramV1._0.Model;
using System;
using System.Windows.Data;

namespace ClassDijagramV1._0.Converters
{
    public class QuarterlyReportsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Reports report = (Reports)value;
            if (report.Type.Equals("Kvartalni izveštaj"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
