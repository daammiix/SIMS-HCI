using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.AccountViewModels.Converters
{
    public class RoleToImageConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            var role = (Role)value;
            if (role != null)
            {
                switch (role)
                {
                    case Role.Secretary:
                        {
                            return App.ProjectPath + "\\Images\\secretary.png";
                        }
                    case Role.Manager:
                        {
                            return App.ProjectPath + "\\Images\\manager.png";
                        }
                    case Role.Patient:
                        {
                            return App.ProjectPath + "\\Images\\patient.png";
                        }
                    case Role.Doctor:
                        {
                            return App.ProjectPath + "\\Images\\doctor.png";
                        }
                }
            }

            return "../Images/acc.png";
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
