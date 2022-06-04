using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Util
{
    public class ValidationErrors : ObservableObject
    {
        private readonly Dictionary<string, string> validationErrors = new Dictionary<string, string>();

        public bool IsValid
        {
            get
            {
                return this.validationErrors.Count < 1;
            }
        }

        public string this[string fieldName]
        {
            get
            {
                return this.validationErrors.ContainsKey(fieldName) ?
                    this.validationErrors[fieldName] : string.Empty;
            }



            set
            {
                if (this.validationErrors.ContainsKey(fieldName))
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        this.validationErrors.Remove(fieldName);
                    }
                    else
                    {
                        this.validationErrors[fieldName] = value;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        this.validationErrors.Add(fieldName, value);
                    }
                }
                this.OnPropertyChanged("IsValid");
            }
        }

        public void Clear()
        {
            validationErrors.Clear();
        }
    }
}
