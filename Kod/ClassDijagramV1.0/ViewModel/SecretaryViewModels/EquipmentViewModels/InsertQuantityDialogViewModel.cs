using ClassDijagramV1._0.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel.SecretaryViewModels.EquipmentViewModels
{
    public class InsertQuantityDialogViewModel
    {
        #region Fields

        // njemu treba da dopunimo quantity
        private PurchaseOrderEquipmentViewModel _purchaseOrderEquipmentViewModel;

        private RelayCommand _okCommand;

        #endregion

        #region Properties

        public int Quantity { get; set; }

        public RelayCommand OkCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new RelayCommand(o => { Finish(o as Window); });
                }

                return _okCommand;
            }
        }

        #endregion

        #region Constructor

        public InsertQuantityDialogViewModel(PurchaseOrderEquipmentViewModel equipment)
        {
            _purchaseOrderEquipmentViewModel = equipment;
        }

        #endregion

        #region Private Helpers

        // Sacuvamo quantity i ugasio dialog
        private void Finish(Window dialog)
        {
            // updatujemo quantity
            if (Quantity == 0) Quantity = 1;
            _purchaseOrderEquipmentViewModel.Quantity = Quantity;

            // zatvorimo dialog
            dialog?.Close();
        }

        #endregion
    }
}
