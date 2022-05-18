using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.ViewModel
{
    public class StorageDrugsViewModel
    {
        private RelayCommand addMedicine;
        private RelayCommand changeMedicine;
        private RelayCommand deleteMedicine;

        MedicineController medicinesController;
        Medicines drug;
        Storage storage;
        RoomController roomController;
        public BindingList<Medicines> AllMedicines { get; set; }
        public BindingList<QuantifiedMedicine> MedicinesList { get; set; }

        public StorageDrugsViewModel()
        {

        }
    }
}
