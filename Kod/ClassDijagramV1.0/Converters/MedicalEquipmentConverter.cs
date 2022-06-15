using ClassDijagramV1._0.Helpers;
using System;
using System.Windows.Data;

namespace ClassDijagramV1._0.Converters
{
    public class MedicalEquipmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            QuantifiedEquipment quantifiedEquipment = (QuantifiedEquipment)value;
            if (quantifiedEquipment.Equipment.EquipmentType.Equals("Medicinska oprema"))
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
