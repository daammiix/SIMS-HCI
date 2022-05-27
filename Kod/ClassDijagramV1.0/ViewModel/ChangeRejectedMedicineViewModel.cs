using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class ChangeRejectedMedicineViewModel
    {
        public RoomController roomController;
        public MedicineController medicineController;
        public Storage storage;
        private IRefreshableMedicineView medicineView;
        public QuantifiedMedicine quantifiedMedicine { get; set; }
        public BindingList<String> MedicineComponents { get; set; }

        private RelayCommand _saveChangedRejectedMedicine;
        private RelayCommand _quitChangededRejectedMedicine;

        public ChangeRejectedMedicineWindow changeRejectedMedicineWindow;

        public ChangeRejectedMedicineViewModel(ChangeRejectedMedicineWindow changeRejectedMedicineWindow, QuantifiedMedicine? quantifiedMedicine, IRefreshableMedicineView medicineView)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            medicineController = app.medicinesController;

            this.changeRejectedMedicineWindow = changeRejectedMedicineWindow;
            this.quantifiedMedicine = quantifiedMedicine;
            this.quantifiedMedicine = (QuantifiedMedicine)quantifiedMedicine;
            this.medicineView = medicineView;

            this.MedicineComponents = quantifiedMedicine.Medicines.MedicineComponents;

            changeRejectedMedicineWindow.ChangeComponents.Clear();

            foreach (var component in MedicineComponents)
            {
                changeRejectedMedicineWindow.ChangeComponents.AppendText(component + Environment.NewLine);
            }

        }

        public RelayCommand SaveChangedRejectedMedicine
        {
            get
            {
                _saveChangedRejectedMedicine = new RelayCommand(o =>
                {
                    ChangeRejectedMedicineAction();
                });

                return _saveChangedRejectedMedicine;
            }
        }

        public RelayCommand QuitChangededRejectedMedicine
        {
            get
            {
                _quitChangededRejectedMedicine = new RelayCommand(o =>
                {
                    changeRejectedMedicineWindow.Close();
                });

                return _quitChangededRejectedMedicine;
            }
        }

        private Medicines MedicineFromTextBoxes()
        {
            var addComponents = changeRejectedMedicineWindow.ChangeComponents.Text;
            MedicineComponents.Clear();
            string[] components = addComponents.Split(',');
            foreach (string c in components)
            {
                var component = c.Trim();
                MedicineComponents.Add(component);
            }
            return new Medicines(changeRejectedMedicineWindow.ChangedDrugsId.Text, changeRejectedMedicineWindow.ChangedDrugsName.Text, changeRejectedMedicineWindow.ChangeDrugsStatus.Text, MedicineComponents);
        }

        private void ChangeRejectedMedicineAction()
        {
            var medicine = MedicineFromTextBoxes();
            var quantity = Int32.Parse(changeRejectedMedicineWindow.ChangedDrugsQuantity.Text);
            String medicineId = quantifiedMedicine.Medicines.ID;

            medicineController.ChangeMedicines(medicine);
            roomController.ChangeStorageMedicineQuantity(medicineId, quantity);

            medicineView.RefreshMedicines();
            changeRejectedMedicineWindow.Close();
        }
    }
}
