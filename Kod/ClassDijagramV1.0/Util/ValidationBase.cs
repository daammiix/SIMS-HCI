using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Util
{
    public abstract class ValidationBase : ObservableObject
    {
        public ValidationErrors ValidationErrors { get; set; }
        public bool IsValid { get; private set; }

        protected ValidationBase()
        {
            this.ValidationErrors = new ValidationErrors();
        }

        protected abstract void ValidateSelf();

        public void Validate()
        {
            this.ValidationErrors.Clear();
            this.ValidateSelf();
            this.IsValid = this.ValidationErrors.IsValid;
            this.OnPropertyChanged("IsValid");
            this.OnPropertyChanged("ValidationErrors");
        }
    }
}
