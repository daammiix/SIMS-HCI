using ClassDijagramV1._0.Util;
using System;

namespace ClassDijagramV1._0.Model
{
    public class QuarterlyReport : ObservableObject
    {
        private String _id;
        private String _name;
        private String _type;
        private int _moneySpent;
        private String _date;

        public String ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public String Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        public int MoneySpent
        {
            get
            {
                return _moneySpent;
            }
            set
            {
                if (value != _moneySpent)
                {
                    _moneySpent = value;
                    OnPropertyChanged("MoneySpent");
                }
            }
        }
        public String Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public QuarterlyReport(String ID, String Name, String Type, int MoneySpent, String Date)
        {
            this._id = ID;
            this._name = Name;
            this._type = Type;
            this._moneySpent = MoneySpent;
            this._date = Date;
        }
    }
}
