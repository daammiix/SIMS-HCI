using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using System;

namespace ClassDijagramV1._0.Model
{
    public class Equipment : ObservableObject
    {
        private String _equipmentID { get; set; }
        private String _name { get; set; }
        private String _equipmentType { get; set; }
        private double _unitPrice { get; set; }
        private UnitsType _units { get; set; }

        public String EquipmentID
        {
            get { return _equipmentID; }
            set
            {
                if (_equipmentID == value) { return; }
                _equipmentID = value;
                OnPropertyChanged("EquipmentID");
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

        public String EquipmentType
        {
            get { return _equipmentType; }
            set
            {
                if (_equipmentType == value) { return; }
                _equipmentType = value;
                OnPropertyChanged("EquipmentType");
            }
        }

        public double UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (_unitPrice == value) { return; }
                _unitPrice = value;
                OnPropertyChanged("UnitPrice");
            }
        }

        public UnitsType Units
        {
            get { return _units; }
            set
            {
                if (_units == value) { return; }
                _units = value;
                OnPropertyChanged("Units");
            }
        }


        public Equipment(String EquipmentID, String Name, String EquipmentType, double price, UnitsType units)
        {
            this.EquipmentID = EquipmentID;
            this.Name = Name;
            this.EquipmentType = EquipmentType;
            this.UnitPrice = price;
            this.Units = units;
        }

        public Equipment()
        {

        }
    }
}
