using ClassDijagramV1._0.Model;
using System;
using System.Windows.Data;

namespace ClassDijagramV1._0.Converters
{
    public class ManagerAppointmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value == "")
            {
                return "";
            }
            var managerAppointment = (ManagerAppointment)value;
            return managerAppointment.Name + ": " + managerAppointment.Start.ToShortTimeString() + " - " + managerAppointment.End.ToShortTimeString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
