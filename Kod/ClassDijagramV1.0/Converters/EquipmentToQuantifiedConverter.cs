using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ClassDijagramV1._0.Helpers;

namespace ClassDijagramV1._0.Converters
{
    public class EquipmentToQuantifiedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var equipment = (RoomEquipmentBinding)value;
            var app = Application.Current as App;
            EquipmentController equipmentController = app.equipmentController;
            var allEquipment = equipmentController.GetAllEquipments();
                foreach (var e in allEquipment)
                {
                    if (e.EquipmentID == equipment.EquipmentID)
                    {
                        return new QuantifiedEquipment(e, equipment.Quantity);
                    }
                }
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
