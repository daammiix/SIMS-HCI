using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.EquipmentViewModels
{
    public class PurchaseOrderEquipmentViewModel : ObservableObject
    {
        #region Fields

        private PurchaseOrderEquipment _purchaseOrderEquipment;

        // Equipment za koji je vezan PurchaseOrderEquipment, posto u modelu imamo Id a ovde nam je potreban objekat
        private Equipment _equipment;

        private EquipmentController _equipmentController;

        #endregion

        #region Properties

        public String Id
        {
            get
            {
                return _equipment.EquipmentID;
            }
        }

        public String Name
        {
            get
            {
                return _equipment.Name;
            }
        }

        public double UnitPrice
        {
            get
            {
                return _equipment.UnitPrice;
            }
        }

        public UnitsType Units
        {
            get
            {
                return _equipment.Units;
            }
        }

        public int Quantity
        {
            get
            {
                return _purchaseOrderEquipment.Quantity;
            }
            set
            {
                if (_purchaseOrderEquipment.Quantity != value)
                {
                    _purchaseOrderEquipment.Quantity = value;
                    OnPropertyChanged("Quantity");
                    // Ako se promeni kolicina promenice se i ukupna cena
                    OnPropertyChanged("TotalPrice");
                }
            }
        }

        public double TotalPrice
        {
            get
            {
                return _purchaseOrderEquipment.Quantity * _equipment.UnitPrice;
            }
        }

        #endregion

        #region Constructor

        public PurchaseOrderEquipmentViewModel(PurchaseOrderEquipment purchaseOrderEquipment)
        {
            _purchaseOrderEquipment = purchaseOrderEquipment;

            App app = Application.Current as App;

            // Uzmemo kontroler
            _equipmentController = app.equipmentController;

            // uzmemo equipment preko kontrolera
            _equipment = _equipmentController.GetEquipmentByID(_purchaseOrderEquipment.EquipmentId);
        }

        #endregion
    }
}
