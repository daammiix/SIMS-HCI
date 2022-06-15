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
    public class AddMedicineViewModel : ObservableObject
    {
        private String _id;
        private String _name;
        private String _quantity;
        private String _medicineComponents;
        public Medicines _suggestedMedicines;

        public RoomController roomController;
        public MedicineController medicineController;

        public Storage storage;
        private IRefreshableMedicineView medicineView;
        QuantifiedMedicine quantifiedMedicine;

        public BindingList<Medicines> MedicinesList { get; set; }
        public BindingList<String> SuggestedMedicines { get; set; }
        public BindingList<String> SelectedMedicines { get; set; }
        public BindingList<String> MedicineComponents { get; set; }

        public String ErrorMessage { get; set; }

        private RelayCommand _saveNewMedicine;
        private RelayCommand _cancelNewMedicine;
        private RelayCommand _addSuggestedMedicine;

        AddMedicineWindow addMedicineWindow;

        public AddMedicineViewModel(AddMedicineWindow addMedicineWindow, IRefreshableMedicineView medicineView)
        {
            var app = Application.Current as App;
            roomController = app.roomController;
            medicineController = app.medicinesController;

            this.addMedicineWindow = addMedicineWindow;
            this.medicineView = medicineView;

            this.MedicinesList = medicineController.GetAllMedicines();

            resetFields();
        }

        public void resetFields()
        {
            _id = null;
            _name = null;
            _quantity = null;
            _medicineComponents = null;
            _suggestedMedicines = null;

            this.SuggestedMedicines = new BindingList<String>();
            this.SelectedMedicines = new BindingList<String>();
            this.MedicineComponents = new BindingList<String>();
        }

        public String ID
        {
            get { return _id; }
            set
            {
                if (_id == value) { return; }
                _id = value;
                if (value.Length < 1)
                {
                    ErrorMessage = "Šifra i naziv ne smeju biti prazni";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }
        public String Name
        {
            get { return _name; }
            set
            {
                if (_name == value) { return; }
                _name = value;
                if (value.Length < 1)
                {
                    ErrorMessage = "Šifra i naziv ne smeju biti prazni";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
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
        public String MedicineComponentsTextBox
        {
            get { return _medicineComponents; }
            set
            {
                if (_medicineComponents == value) { return; }
                _medicineComponents = value;
                if (value.Length < 1)
                {
                    ErrorMessage = "Šifra i naziv ne smeju biti prazni";
                    OnPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }
        public Medicines SuggestedMedicinesTextBox
        {
            get { return _suggestedMedicines; }
            set
            {
                if (_suggestedMedicines == value) { return; }
                _suggestedMedicines = value;
            }
        }

        public RelayCommand SaveNewMedicine
        {
            get
            {
                _saveNewMedicine = new RelayCommand(o =>
                {
                    if(_name == null || _id == null || _quantity == null || _medicineComponents == null
                       || _name == "" || _id == "" || _quantity == "" || _medicineComponents == "")
                    {
                        ErrorMessage = "Polja nisu popunjena";
                        OnPropertyChanged("ErrorMessage");
                        return;
                    }
                    SaveNewMedicineAction();
                });

                return _saveNewMedicine;
            }
        }

        public RelayCommand CancelNewMedicine
        {
            get
            {
                _cancelNewMedicine = new RelayCommand(o =>
                {
                    resetFields();
                    addMedicineWindow.Close();
                });

                return _cancelNewMedicine;
            }
        }

        public RelayCommand AddSuggestedMedicine
        {
            get
            {
                _addSuggestedMedicine = new RelayCommand(o =>
                {
                    AddSuggestedMedicineAction();
                });

                return _addSuggestedMedicine;
            }
        }

        private Medicines MedicineFromTextBoxes()
        {
            var addComponents = MedicineComponentsTextBox;
            string[] components = addComponents.Split(',');
            foreach (string c in components)
            {
                var component = c.Trim();
                MedicineComponents.Add(component);
            }
            return new Medicines(ID, Name, "Na čekanju", MedicineComponents, SuggestedMedicines);
        }

        private void SaveNewMedicineAction()
        {
            var medicine = MedicineFromTextBoxes();
            var quantity = Int32.Parse(Quantity);
            medicineController.AddMedicines(medicine);
            ((Storage)roomController.GetRoom("storage")).addNewMedicine(medicine, quantity);
            medicineView.RefreshMedicines();
            resetFields();
            addMedicineWindow.Close();
        }

        private void AddSuggestedMedicineAction()
        {
            Medicines medicine = (Medicines)SuggestedMedicinesTextBox;
            SuggestedMedicines.Add(medicine.ID);
            SelectedMedicines.Add(medicine.ID + " " + medicine.Name);
        }
    }
}
