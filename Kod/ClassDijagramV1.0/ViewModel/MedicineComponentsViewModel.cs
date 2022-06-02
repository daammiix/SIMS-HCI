using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using System;
using System.ComponentModel;

namespace ClassDijagramV1._0.ViewModel
{
    public class MedicineComponentsViewModel
    {
        public BindingList<String> MedicineComponents { get; set; }
        private RelayCommand _closeMedicineComponents;
        MedicineComponentsWindow medicineComponentsWindow;

        public MedicineComponentsViewModel(MedicineComponentsWindow medicineComponentsWindow, QuantifiedMedicine? quantifiedMedicine)
        {
            this.medicineComponentsWindow = medicineComponentsWindow;
            this.MedicineComponents = quantifiedMedicine.Medicines.MedicineComponents;
            medicineComponentsWindow.Components.Clear();

            foreach (var component in MedicineComponents)
            {
                medicineComponentsWindow.Components.AppendText(component + Environment.NewLine);
            }
        }

        public RelayCommand CloseListOfEquipment
        {
            get
            {
                _closeMedicineComponents = new RelayCommand(o =>
                {
                    medicineComponentsWindow.Close();
                });

                return _closeMedicineComponents;
            }
        }
    }
}
