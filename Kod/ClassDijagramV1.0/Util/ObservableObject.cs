using System;
using System.ComponentModel;

namespace ClassDijagramV1._0.Util
{
    public class ObservableObject : ValidationErrorContainer, INotifyPropertyChanged
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
