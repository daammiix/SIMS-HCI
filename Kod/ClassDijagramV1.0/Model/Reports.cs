using ClassDijagramV1._0.Util;
using System;

namespace ClassDijagramV1._0.Model
{
    public class Reports : ObservableObject
    {
        private String _id;
        private String _type;
        private String _name;
        private String _date;

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

        public String Type
        {
            get { return _type; }
            set
            {
                if (_type == value) { return; }
                _type = value;
                OnPropertyChanged("Type");
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

        public String Date
        {
            get { return _date; }
            set
            {
                if (_date == value) { return; }
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        public Reports(String ID, String Type, String Name, String Date)
        {
            this.ID = ID;
            this.Type = Type;
            this.Name = Name;
            this.Date = Date;
        }
    }
}
