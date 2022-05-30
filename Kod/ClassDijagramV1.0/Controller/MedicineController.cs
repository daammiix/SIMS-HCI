using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Controller
{
    public class MedicineController
    {
        private MedicinesService medicinesService;

        public MedicineController(MedicinesService medicinesService)
        {
            this.medicinesService = medicinesService;
        }

        public void AddMedicines(Medicines medicines)
        {
            medicinesService.AddMedicines(medicines);
        }

        public void DeleteMedicines(String medicinesID)
        {
            medicinesService.DeleteMedicines(medicinesID);
        }

        public void ChangeMedicines(Medicines medicines)
        {
            medicinesService.ChangeMedicines(medicines);
        }

        public BindingList<Medicines> GetAllMedicines()
        {
            return medicinesService.GetAllMedicines();
        }

        public Medicines? GetMedicinesByID(String MedicinesID)
        {
            return medicinesService.GetAMedicines(MedicinesID);
        }
    }
}
