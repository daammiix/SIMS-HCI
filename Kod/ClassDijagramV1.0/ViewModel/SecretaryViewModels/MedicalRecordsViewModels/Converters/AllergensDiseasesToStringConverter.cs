using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.MedicalRecordsViewModels.Converters
{
    // Da bi u row details mogao Property Text da se bajndujes
    public class AllergensDiseasesToStringConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Castujemo value u listu stringova posto su nam to allergeni
            List<string> items = value as List<string>;

            StringBuilder ret = new StringBuilder();
            if (items != null)
            {
                foreach (string item in items)
                {
                    item.Trim();
                    ret.Append(item);
                    ret.Append(", ");
                }
            }

            // Skinemo sa stringBuildera na kraju ", " ako uopste imamo alergena
            if (ret.Length > 0)
            {
                ret.Remove(ret.Length - 2, 2);
            }
            else
            {
                // Ako nema nijednog alergena stavimo "/"
                ret.Append("/");
            }

            // Vratimo string koji predstavlja sve allergene
            return ret.ToString();
        }

        // Od stringa napravimo listu
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // castujemo u string
            string items = (string)value;
            // Trimujemo
            items.Trim();

            // ret
            List<string> ret = new List<string>();

            // Ako je '/' znaci da nema alergena i vratimo praznu listu
            if (items.Equals("/"))
            {
                return ret;
            }

            // proverimo da li smo uopste nesto uneli
            if (items.Length > 0)
            {
                // splitujemo po ','
                string[] itemsSplitted = items.Split(',');

                // Trimujemo sve
                foreach (string item in itemsSplitted)
                {
                    item.Trim();
                }

                // Dodamo sve u listu
                ret.AddRange(itemsSplitted);
            }
            // Dodamo '/' ako nemamo nista
            else
            {
                ret.Append("/");
            }

            return ret;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
