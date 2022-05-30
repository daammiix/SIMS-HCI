using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class DeleteMedicineViewModel
    {
        public RoomController roomController;
        public MedicineController medicineController;

        public Storage storage;
        private IRefreshableMedicineView medicineView;
        QuantifiedMedicine quantifiedMedicine;

        private RelayCommand _deleteYesMedicine;
        private RelayCommand _deleteNoMedicine;

        DeleteMedicineWindow deleteMedicineWindow;

        public DeleteMedicineViewModel(DeleteMedicineWindow deleteMedicineWindow, QuantifiedMedicine? quantifiedMedicine, IRefreshableMedicineView medicineView)
        {
            var app = Application.Current as App;
            medicineController = app.medicinesController;
            roomController = app.roomController;

            this.deleteMedicineWindow = deleteMedicineWindow;
            this.quantifiedMedicine = (QuantifiedMedicine)quantifiedMedicine;
            this.medicineView = medicineView;
        }

        public int findMedicine(String medicineId)
        {
            int index = 0;
            var medicineList = ((Storage)roomController.GetRoom("storage")).MedicineList;
            foreach (var medicine in medicineList)
            {
                if (medicine.MedicineID == medicineId)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public RelayCommand DeleteYesMedicine
        {
            get
            {
                _deleteYesMedicine = new RelayCommand(o =>
                {
                    DeleteMedicineAction();
                });

                return _deleteYesMedicine;
            }
        }

        public RelayCommand DeleteNoMedicine
        {
            get
            {
                _deleteNoMedicine = new RelayCommand(o =>
                {
                    deleteMedicineWindow.Close();
                });

                return _deleteNoMedicine;
            }
        }

        private void DeleteMedicineAction()
        {
            String medicineId = quantifiedMedicine.Medicines.ID;
            int index = findMedicine(medicineId);
            medicineController.DeleteMedicines(medicineId);
            if (index == -1)
            {
                throw new Exception();
            }
            ((Storage)roomController.GetRoom("storage")).MedicineList.RemoveAt(index);
            medicineView.RefreshMedicines();
            deleteMedicineWindow.Close();
        }
    }
}
