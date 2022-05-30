using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.EquipmentViewModels
{
    public class PurchaseOrderViewModel : ObservableObject
    {
        #region Fields

        private PurchaseOrder _purchaseOrder;

        #endregion

        #region Properties

        // Oprema iz porudzbine
        public ObservableCollection<PurchaseOrderEquipmentViewModel> Equipment { get; set; }

        public int Id
        {
            get { return _purchaseOrder.Id; }
        }

        public string SupplierName
        {
            get
            {
                return _purchaseOrder.SupplierName;
            }
            set
            {
                if (_purchaseOrder.SupplierName != value)
                {
                    _purchaseOrder.SupplierName = value;
                    OnPropertyChanged("SpplierName");
                }
            }
        }

        public string Description
        {
            get
            {
                return _purchaseOrder.Description;
            }
            set
            {
                if (_purchaseOrder.Description != value)
                {
                    _purchaseOrder.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public double Price
        {
            get
            {
                return _purchaseOrder.Price;
            }
            set
            {
                if (_purchaseOrder.Price != value)
                {
                    _purchaseOrder.Price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        public DateTime DeliveryTime
        {
            get
            {
                return _purchaseOrder.DeliveryTime;
            }
            set
            {
                if (_purchaseOrder.DeliveryTime != value)
                {
                    _purchaseOrder.DeliveryTime = value;
                    OnPropertyChanged("DeliveryTime");
                }
            }
        }

        #endregion

        #region Constructor

        public PurchaseOrderViewModel(PurchaseOrder purchaseOrder)
        {
            _purchaseOrder = purchaseOrder;

            Equipment = new ObservableCollection<PurchaseOrderEquipmentViewModel>();

            foreach (PurchaseOrderEquipment purchaseOrderEquipment in _purchaseOrder.Equipment)
            {
                Equipment.Add(new PurchaseOrderEquipmentViewModel(purchaseOrderEquipment));
            }

            // Racuna cenu narudzbine 
            CalculatePrice();
        }


        #endregion

        #region Private Helpers

        private void CalculatePrice()
        {
            // Ako ne stavimo ovu liniju, svaki put kad se ucita tab cena ce da se duplira
            _purchaseOrder.Price = 0;
            foreach (PurchaseOrderEquipmentViewModel purchaseOrderEquipmentViewModel in Equipment)
            {
                _purchaseOrder.Price += purchaseOrderEquipmentViewModel.TotalPrice;
            }
        }

        #endregion
    }
}
