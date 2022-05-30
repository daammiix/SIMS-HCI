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
    public class StorageDrugsViewModel : IRefreshableMedicineView
    {
        private RelayCommand _addMedicine;
        private RelayCommand _changeMedicine;
        private RelayCommand _deleteMedicine;
        private RelayCommand _openComponents;

        MedicineController medicinesController;
        Medicines drug;
        Storage storage;
        RoomController roomController;
        public BindingList<Medicines> AllMedicines { get; set; }
        public BindingList<QuantifiedMedicine> MedicinesList { get; set; }

        public QuantifiedMedicine selectedMedicine { get; set; }

        String _searchText;
        public String SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_searchText == value)
                {
                    return;
                }
                _searchText = value;
                RefreshMedicines();
            }
        }

        public StorageDrugsViewModel()
        {
            var app = Application.Current as App;
            MedicinesList = new BindingList<QuantifiedMedicine>();
            medicinesController = app.medicinesController;
            roomController = app.roomController;
            AllMedicines = medicinesController.GetAllMedicines();
            storage = (Storage)roomController.GetRoom("storage");
            _searchText = "";
            RefreshMedicines();
        }

        public RelayCommand AddMedicine
        {
            get
            {

                _addMedicine = new RelayCommand(o =>
                {
                    ShowAddMedicineDialog();
                });

                return _addMedicine;
            }
        }

        public RelayCommand ChangeMedicine
        {
            get
            {
                _changeMedicine = new RelayCommand(o =>
                {
                    ShowChangeMEdicineDialog();
                });

                return _changeMedicine;
            }
        }

        public RelayCommand DeleteMedicine
        {
            get
            {
                _deleteMedicine = new RelayCommand(o =>
                {
                    ShowDeleteMedicineDialog();
                });

                return _deleteMedicine;
            }
        }

        public RelayCommand OpenComponents
        {
            get
            {
                _openComponents = new RelayCommand(o =>
                {
                    OpenComponentsAction();
                });

                return _openComponents;
            }
        }

        private void ShowAddMedicineDialog()
        {
            AddMedicineWindow addMedicineWindow = new AddMedicineWindow(this);
            addMedicineWindow.Show();
        }

        private void ShowChangeMEdicineDialog()
        {
            QuantifiedMedicine? quantifiedMedicine = (QuantifiedMedicine?)selectedMedicine;
            if (quantifiedMedicine != null)
            {
                if (quantifiedMedicine.Medicines.Status.Equals("Odobren"))
                {
                    ChangeMedicineWindow changeMedicineWindow = new ChangeMedicineWindow(quantifiedMedicine, this);
                    changeMedicineWindow.Show();
                }
                else
                {
                    ChangeRejectedMedicineWindow changeRejectedMedicineWindow = new ChangeRejectedMedicineWindow(quantifiedMedicine, this);
                    changeRejectedMedicineWindow.Show();
                }
            }
            else
            {
                Warning warning = new Warning();
                warning.Show();
            }
        }

        private void ShowDeleteMedicineDialog()
        {
            QuantifiedMedicine? quantifiedMedicine = (QuantifiedMedicine?)selectedMedicine;
            if (quantifiedMedicine != null)
            {
                DeleteMedicineWindow deleteMedicineWindow = new DeleteMedicineWindow(quantifiedMedicine, this);
                deleteMedicineWindow.Show();
            }
            else
            {
                Warning warning = new Warning();
                warning.Show();
            }
        }

        private void OpenComponentsAction()
        {
            QuantifiedMedicine? quantifiedMedicine = (QuantifiedMedicine?)selectedMedicine;
            MedicineComponentsWindow medicineComponents = new MedicineComponentsWindow(quantifiedMedicine);
            medicineComponents.Show();
        }

        public void RefreshMedicines()
        {
            MedicinesList.Clear();
            foreach (var binding in storage.MedicineList)
            {
                foreach (var e in AllMedicines)
                {
                    if (e.ID == binding.MedicineID)
                    {
                        String search = _searchText.ToLower();
                        if (
                            !e.ID.ToLower().Contains(search) &&
                            !e.Name.ToLower().Contains(search) &&
                            !e.Status.ToLower().Contains(search) &&
                            !binding.Quantity.ToString().Contains(search)
                        )
                        {
                            continue;
                        }
                        MedicinesList.Add(new QuantifiedMedicine(e, binding.Quantity));
                    }
                }
            }
        }
    }
}
