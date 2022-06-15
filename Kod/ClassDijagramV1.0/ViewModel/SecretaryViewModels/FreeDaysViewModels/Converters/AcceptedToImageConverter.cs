using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.FreeDaysViewModels.Converters
{
    public class AcceptedToImageConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool accepted = (bool)value;

            if (accepted)
            {
                return "../Images/checked.png";
            }
            else
            {
                return "../Images/refused.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
