using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.EquipmentViewModels
{
    public class EquipmentViewModel : ObservableObject
    {
        #region Fields

        // equipment za koji je vezan
        private Equipment _equipment;

        #endregion

        #region Properties

        public string Id
        {
            get
            {
                return _equipment.EquipmentID;
            }
            set
            {
                if (_equipment.EquipmentID != value)
                {
                    _equipment.EquipmentID = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Name
        {
            get
            {
                return _equipment.Name;
            }
            set
            {
                if (_equipment.Name != value)
                {
                    _equipment.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public double UnitPrice
        {
            get
            {
                return _equipment.UnitPrice;
            }
            set
            {
                if (_equipment.UnitPrice != value)
                {
                    _equipment.UnitPrice = value;
                    OnPropertyChanged("UnitPrice");
                }
            }
        }

        public UnitsType Units
        {
            get
            {
                return _equipment.Units;
            }
            set
            {
                if (_equipment.Units != value)
                {
                    _equipment.Units = value;
                    OnPropertyChanged("Units");
                }
            }
        }

        #endregion

        #region Constructor

        public EquipmentViewModel(Equipment equipment)
        {
            _equipment = equipment;
        }

        #endregion

    }
}
