using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using System;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class ChangeMedicineViewModel
    {
        public RoomController roomController;
        public MedicineController medicineController;
        public Storage storage;
        private IRefreshableMedicineView medicineView;
        public QuantifiedMedicine quantifiedMedicine { get; set; }

        private RelayCommand _saveChangedMedicine;
        private RelayCommand _quitChangededMedicine;

        public ChangeMedicineWindow changeMedicineWindow;

        public ChangeMedicineViewModel(ChangeMedicineWindow changeMedicineWindow, QuantifiedMedicine? quantifiedMedicine, IRefreshableMedicineView medicineView)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            medicineController = app.medicinesController;

            this.changeMedicineWindow = changeMedicineWindow;
            this.quantifiedMedicine = (QuantifiedMedicine)quantifiedMedicine;
            this.medicineView = medicineView;
        }

        public RelayCommand SaveChangedMedicine
        {
            get
            {
                _saveChangedMedicine = new RelayCommand(o =>
                {
                    ChangeMedicineAction();
                });

                return _saveChangedMedicine;
            }
        }

        public RelayCommand QuitChangededMedicine
        {
            get
            {
                _quitChangededMedicine = new RelayCommand(o =>
                {
                    changeMedicineWindow.Close();
                });

                return _quitChangededMedicine;
            }
        }

        private Medicines MedicineFromTextBoxes()
        {
            return new Medicines(changeMedicineWindow.ChangedDrugsId.Text,
                                 changeMedicineWindow.ChangedDrugsName.Text,
                                 changeMedicineWindow.ChangeDrugsStatus.Text);
        }

        private void ChangeMedicineAction()
        {
            var medicine = MedicineFromTextBoxes();
            var quantity = Int32.Parse(changeMedicineWindow.ChangedDrugsQuantity.Text);
            String medicineId = quantifiedMedicine.Medicines.ID;

            medicineController.ChangeMedicines(medicine);
            roomController.ChangeStorageMedicineQuantity(medicineId, quantity);

            medicineView.RefreshMedicines();
            changeMedicineWindow.Close();
        }
    }
}
