using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using System;
using System.ComponentModel;

namespace ClassDijagramV1._0.Service
{
    public class MedicinesService
    {
        private MedicinesRepo medicinesRepo;
        public MedicinesService(MedicinesRepo medicinesRepo)
        {
            this.medicinesRepo = medicinesRepo;
        }

        public void AddMedicines(Medicines medicines)
        {
            if (this.CheckIfUniq(medicines, false))
            {
                medicinesRepo.CreateNewMedicines(medicines);
            }
        }

        public void DeleteMedicines(String medicinesID)
        {
            medicinesRepo.DeleteMedicines(medicinesID);
        }

        public void ChangeMedicines(Medicines medicines)
        {
            if (this.CheckIfUniq(medicines, true))
            {
                medicinesRepo.SetMedicines(medicines);
            }
        }

        public Medicines? GetAMedicines(String medicinesID)
        {
            return medicinesRepo.GetMedicines(medicinesID);
        }

        public BindingList<Medicines> GetAllMedicines()
        {
            return medicinesRepo.GetAllMedicines();
        }

        private Boolean CheckIfUniq(Medicines medicines, bool existingMedicines)
        {
            var medicinesList = medicinesRepo.GetAllMedicines();
            foreach (var medicine in medicinesList)
            {
                if (!existingMedicines && medicines.ID == medicine.ID)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
