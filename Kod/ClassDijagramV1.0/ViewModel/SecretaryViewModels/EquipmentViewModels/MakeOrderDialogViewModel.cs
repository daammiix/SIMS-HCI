using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.EquipmentViewModels
{
    public class MakeOrderDialogViewModel
    {
        #region Fields

        private RelayCommand _makeOrderCommand;

        private EquipmentController _equipmentController;

        private ObservableCollection<PurchaseOrderViewModel> _purchaseOrders;

        private PurchaseOrderController _purchaseOrderController;

        #endregion

        #region Properties

        // Dostupna oprema za naruvianje
        public ObservableCollection<EquipmentViewModel> AvailableEquipment { get; set; }

        // Oprema u porudzbenici
        public ObservableCollection<PurchaseOrderEquipmentViewModel> EquipmentInOrder { get; set; }

        public RelayCommand MakeOrderCommand
        {
            get
            {
                if (_makeOrderCommand == null)
                {
                    _makeOrderCommand = new RelayCommand(o => MakeOrder(o as Window), MakeOrderCanExecute);
                }

                return _makeOrderCommand;
            }
        }

        public string SupplierName { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Description { get; set; }

        #endregion

        #region Constructor

        public MakeOrderDialogViewModel(ObservableCollection<PurchaseOrderViewModel> purchaseOrders)
        {
            _purchaseOrders = purchaseOrders;

            // Ucitamo sve equipmente
            App app = Application.Current as App;

            _equipmentController = app.equipmentController;
            _purchaseOrderController = app.PurchaseOrderController;

            EquipmentInOrder = new ObservableCollection<PurchaseOrderEquipmentViewModel>();

            // Od svakog napravimo EquipmentViewModel i dodamo u listu dostupnih equipmenta
            AvailableEquipment = new ObservableCollection<EquipmentViewModel>();
            foreach (Equipment equipment in _equipmentController.GetAllEquipments())
            {
                AvailableEquipment.Add(new EquipmentViewModel(equipment));
            }
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Pravi order na osnovu inputa i gasi dialog
        /// </summary>
        private void MakeOrder(Window dialog)
        {
            // Napravimo listu purchaseOrderEquipmenta
            List<PurchaseOrderEquipment> purchaseOrderEquipment = new List<PurchaseOrderEquipment>();
            // Popunimo listu
            foreach (PurchaseOrderEquipmentViewModel equipmentViewModel in EquipmentInOrder)
            {
                purchaseOrderEquipment.Add(new PurchaseOrderEquipment(equipmentViewModel.Id, equipmentViewModel.Quantity));
            }
            PurchaseOrder purchaseOrder = new PurchaseOrder(SupplierName, purchaseOrderEquipment, Description, DeliveryTime); ;
            PurchaseOrderViewModel purchaseOrderViewModel = new PurchaseOrderViewModel(purchaseOrder);

            // Dodamo novu narudzbinu preko controllera u repozitorijum i njen view model 
            _purchaseOrders.Add(purchaseOrderViewModel);
            _purchaseOrderController.AddPurchaseOrder(purchaseOrder);

            dialog.Close();
        }

        /// <summary>
        /// Proverava da li komanda moze da se izvrsi
        /// </summary>
        /// <returns></returns>
        private bool MakeOrderCanExecute()
        {
            if (EquipmentInOrder.Count > 0 && SupplierName != "" && DeliveryTime != DateTime.MinValue)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
