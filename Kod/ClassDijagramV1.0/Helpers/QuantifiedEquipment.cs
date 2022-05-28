using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Helpers
{
    public class QuantifiedEquipment : ObservableObject
    {
        private Equipment _equipment;
        private int _quantity;

        public Equipment Equipment
        {
            get { return _equipment; }
            set
            {
                if (_equipment == value) { return; }
                _equipment = value;
                OnPropertyChanged("Equipment");
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity == value) { return; }
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public QuantifiedEquipment(Equipment e, int q)
        {
            Equipment = e;
            Quantity = q;
        }
    }
}
