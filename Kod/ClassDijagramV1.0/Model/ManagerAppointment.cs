using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ClassDijagramV1._0.Model
{
    public class ManagerAppointment : ObservableObject
    {
        private String _id;
        private String _name;
        private DateTime _start;
        private DateTime _end;

        public String ID
        {
            get { return _id; }
            set
            {
                if (_id == value) { return; }
                _id = value;
                OnPropertyChanged("ID");
            }
        }
        public String Name
        {
            get { return _name; }
            set
            {
                if (_name == value) { return; }
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public DateTime Start
        {
            get {
                return _start;
            }
            set
            {
                if (_start == value) { return; }
                _start = value;
                OnPropertyChanged("Start");
            }
        }
        public DateTime End
        {
            get { return _end; }
            set
            {
                if (_end == value) { return; }
                _end = value;
                OnPropertyChanged("End");
            }
        }

        [JsonIgnore]
        public Brush Color
        {
            get { return new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 30, 50)); }
        }

        public ManagerAppointment(String ID, String Name, DateTime Start, DateTime End)
        {
            _id = ID;
            _name = Name;
            _start = Start;
            _end = End;
        }

        public ManagerAppointment()
        { }
    }
}
