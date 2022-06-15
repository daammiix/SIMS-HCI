using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClassDijagramV1._0.Util.ValidationRules
{
    public class NumberRule : ValidationRule
    {
        public string Name { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string val = value as string;
            int duration;
            if (int.TryParse(val, out duration))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, $"{Name} must be number.");
            }
        }
    }
}
