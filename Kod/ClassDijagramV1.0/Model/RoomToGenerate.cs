using ClassDijagramV1._0.Util;
using System;

namespace ClassDijagramV1._0.Model
{
    public class RoomToGenerate : ObservableObject
    {
        private String _id;
        private String _name;
        private String _start;
        private String _end;

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

        public String Start
        {
            get { return _start; }
            set
            {
                if (_start == value) { return; }
                _start = value;
                OnPropertyChanged("Start");
            }
        }

        public String End
        {
            get { return _end; }
            set
            {
                if (_end == value) { return; }
                _end = value;
                OnPropertyChanged("End");
            }
        }

        public RoomToGenerate(String ID, String Name, String Start, String End)
        {
            this._id = ID;
            this._name = Name;
            this._start = Start;
            this._end = End;
        }
    }
}
