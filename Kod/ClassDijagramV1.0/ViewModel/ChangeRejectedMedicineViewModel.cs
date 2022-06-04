using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using System;
using System.ComponentModel;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class ChangeRejectedMedicineViewModel
    {
        private String _id;
        private String _name;
        private String _status;
        private String _quantity;
        private String _medicineComponents;

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

            this.ID = quantifiedMedicine.Medicines.ID;
            this.Name = quantifiedMedicine.Medicines.Name;
            this.Status = quantifiedMedicine.Medicines.Status;
            this.Quantity = quantifiedMedicine.Quantity.ToString();
            foreach(var component in quantifiedMedicine.Medicines.MedicineComponents)
            {
                this.MedicineComponentsTextBox += this.MedicineComponentsTextBox + component + "\n";
            }
        }

        public String ID
        {
            get { return _id; }
            set
            {
                if (_id == value) { return; }
                _id = value;
            }
        }
        public String Name
        {
            get { return _name; }
            set
            {
                if (_name == value) { return; }
                _name = value;
            }
        }
        public String Status
        {
            get { return _status; }
            set
            {
                if (_status == value) { return; }
                _status = value;
            }
        }
        public String Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity == value) { return; }
                _quantity = value;
            }
        }
        public String MedicineComponentsTextBox
        {
            get { return _medicineComponents; }
            set
            {
                if (_medicineComponents == value) { return; }
                _medicineComponents = value;
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
            var addComponents = MedicineComponentsTextBox;
            MedicineComponents.Clear();
            string[] components = addComponents.Split(',');
            foreach (string c in components)
            {
                var component = c.Trim();
                MedicineComponents.Add(component);
            }
            return new Medicines(ID, Name, Status, MedicineComponents);
        }

        private void ChangeRejectedMedicineAction()
        {
            var medicine = MedicineFromTextBoxes();
            var quantity = Int32.Parse(Quantity);

            medicineController.ChangeMedicines(medicine);
            roomController.ChangeStorageMedicineQuantity(ID, quantity);

            medicineView.RefreshMedicines();
            changeRejectedMedicineWindow.Close();
        }
    }
}
