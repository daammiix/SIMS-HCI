using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Util
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifikuje da se property promenio
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }
}
