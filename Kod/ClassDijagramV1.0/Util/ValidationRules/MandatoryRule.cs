using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Util.ValidationRules
{
    public class MandatoryRule : ValidationRule
    {
        public string Name
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            DateTime? dateTime = value as DateTime?;
            string val;
            if (dateTime.HasValue)
            {
                val = dateTime.Value.ToShortDateString();
            }
            else
            {
                val = (string)value;
            }
            if (String.IsNullOrEmpty(val))
            {
                if (Name.Length == 0)
                    Name = "Field";
                return new ValidationResult(false, Name + " is mandatory.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
