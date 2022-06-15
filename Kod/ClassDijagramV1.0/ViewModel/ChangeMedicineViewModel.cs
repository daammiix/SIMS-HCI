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
    public class ChangeMedicineViewModel : ObservableObject
    {
        private String _id;
        private String _name;
        private String _status;
        private String _quantity;

        public RoomController roomController;
        public MedicineController medicineController;
        public Storage storage;
        private IRefreshableMedicineView medicineView;
        public QuantifiedMedicine quantifiedMedicine { get; set; }

        public String ErrorMessage { get; set; }

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

            this.ID = quantifiedMedicine.Medicines.ID;
            this.Name = quantifiedMedicine.Medicines.Name;
            this.Status = quantifiedMedicine.Medicines.Status;
            this.Quantity = quantifiedMedicine.Quantity.ToString();
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
                int quantity;
                bool is_number = int.TryParse(value, out quantity);
                if (!is_number)
                {
                    ErrorMessage = "Uneta vrednost mora biti broj";
                    OnPropertyChanged("ErrorMessage");
                }
                else if (quantity < 1)
                {
                    ErrorMessage = "Broj mora biti veći od 0";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }

        public RelayCommand SaveChangedMedicine
        {
            get
            {
                _saveChangedMedicine = new RelayCommand(o =>
                {
                    if(_quantity == "")
                    {
                        ErrorMessage = "Polja nisu popunjena";
                        OnPropertyChanged("ErrorMessage");
                        return;
                    }
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
            return new Medicines(ID, Name, Status);
        }

        private void ChangeMedicineAction()
        {
            var medicine = MedicineFromTextBoxes();
            var quantity = Int32.Parse(Quantity);

            medicineController.ChangeMedicines(medicine);
            roomController.ChangeStorageMedicineQuantity(ID, quantity);

            medicineView.RefreshMedicines();
            changeMedicineWindow.Close();
        }
    }
}
