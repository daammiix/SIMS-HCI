using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.SecretaryView.EquipmentView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.EquipmentViewModels
{
    public class EquipmentMainViewModel : ObservableObject
    {
        #region Fields

        private RelayCommand _searchCommand;
        private RelayCommand _makeOrderCommand;
        private EquipmentController _equipmentController;
        private PurchaseOrderController _purchaseOrderController;

        private PurchaseOrderViewModel _selectedPurchaseOrder;

        #endregion

        #region Properties 

        // Sve narudzbine
        public ObservableCollection<PurchaseOrderViewModel> PurchaseOrders { get; set; }

        public string SearchText { get; set; }
        public PurchaseOrderViewModel SelectedPurchaseOrder
        {
            get { return _selectedPurchaseOrder; }
            set
            {
                if (_selectedPurchaseOrder != value)
                {
                    _selectedPurchaseOrder = value;
                    OnPropertyChanged("SelectedPurchaseOrder");
                }
            }
        }

        public RelayCommand MakeOrderCommand
        {
            get
            {
                if (_makeOrderCommand == null)
                {
                    _makeOrderCommand = new RelayCommand(o => { ShowMakeOrderDialog(); });
                }

                return _makeOrderCommand;
            }
        }

        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(o => SearchExecute(o as ListView));
                }

                return _searchCommand;
            }
        }

        #endregion

        #region Constructor

        public EquipmentMainViewModel()
        {
            App app = Application.Current as App;

            // Uzmemo potrebne kontrolere
            _equipmentController = app.equipmentController;
            _purchaseOrderController = app.PurchaseOrderController;

            PurchaseOrders = new ObservableCollection<PurchaseOrderViewModel>();

            // Popunimo observable kolekciju sa viewModelima narudzbina
            foreach (PurchaseOrder purchaseOrder in _purchaseOrderController.GetPurchaseOrders())
            {
                PurchaseOrders.Add(new PurchaseOrderViewModel(purchaseOrder));
            }

            // Pokrenemo timer za proveru da li su porudzbine gotove
            StartTimer();
        }

        #endregion

        #region Private Helpers

        // Prikazuje dialog za pravljenje narudzbine
        private void ShowMakeOrderDialog()
        {
            App app = Application.Current as App;

            MakeOrderDialog dialog = new MakeOrderDialog();

            dialog.Owner = app.MainWindow;
            // Stavimo data context i prosledimo narudzbine da bi dodalu novu kad je napravimo
            dialog.DataContext = new MakeOrderDialogViewModel(PurchaseOrders);
            dialog.ShowDialog();
        }

        /// <summary>
        /// Pokrece timer
        /// </summary>
        private void StartTimer()
        {
            Timer timer = new Timer(1000);
            timer.Elapsed += (sender, e) => CheckForComplitedDeliveries();
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        /// <summary>
        /// Brise narudzbine koje su zavrsene i poziva servis kako bi prebacio opremu u magacin i izbrisao narudzbinu
        /// iz repozitorijuma
        /// </summary>
        private void CheckForComplitedDeliveries()
        {
            // Lista purchaseOrdera za brisanje
            List<PurchaseOrderViewModel> completedPurchaseOrders = new List<PurchaseOrderViewModel>();

            foreach (PurchaseOrderViewModel purchaseOrderVM in PurchaseOrders)
            {
                if (purchaseOrderVM.DeliveryTime < DateTime.Now)
                {
                    // Dodamo za brisanje
                    completedPurchaseOrders.Add(purchaseOrderVM);

                    // Izbrisemo iz repozitorijuma preko kontrolera
                    PurchaseOrder purchaseOrder = _purchaseOrderController.RemovePurchaseOrder(purchaseOrderVM.Id);

                    // Premestimo opremu iz narudzbine u magacin
                    _purchaseOrderController.MoveEquipmentToStorage(purchaseOrder);

                }
            }
            // Izbrisemo zavrsene narudzbine iz view-a, moramo da brisemo sa dispatcher thread-a jer kolekcije
            // ne dozvoljavaju da se brise sa drugog thread-a
            if (App.Current != null)
            {
                App.Current.Dispatcher.BeginInvoke(() =>
                {
                    foreach (PurchaseOrderViewModel purchaseOrderVM in completedPurchaseOrders)
                    {
                        PurchaseOrders.Remove(purchaseOrderVM);
                    }
                });
            }

        }

        private void SearchExecute(ListView lw)
        {
            if (!SearchText.Equals(""))
            {
                DateTime searchedDateTime;
                DateTime.TryParse(SearchText, out searchedDateTime);

                var filteredPurchaseOrders = PurchaseOrders
                    .Where(item => item.SupplierName.ToLower().Contains(SearchText.ToLower())).ToList();

                if (searchedDateTime != null)
                {
                    foreach (PurchaseOrderViewModel purchaseOrderVM in PurchaseOrders)
                    {
                        if (purchaseOrderVM.DeliveryTime.ToShortDateString().Equals(searchedDateTime.ToShortDateString()))
                        {
                            filteredPurchaseOrders.Add(purchaseOrderVM);
                        }
                    }
                    // filteredPurchaseOrders.Concat(PurchaseOrders
                    //    .Where(item => item.DeliveryTime.ToShortDateString().Equals(searchedDateTime.ToShortDateString())));
                }

                lw.ItemsSource = filteredPurchaseOrders;
            }
            else
            {
                lw.ItemsSource = PurchaseOrders;
            }
        }

        #endregion
    }
}
