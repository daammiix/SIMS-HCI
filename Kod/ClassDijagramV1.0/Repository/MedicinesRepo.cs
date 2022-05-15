using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class MedicinesRepo
    {
        private String path = "..\\..\\..\\Data\\medicines.json";
        private BindingList<Medicines> medicinesList = new BindingList<Medicines>();

        public MedicinesRepo()
        {
            string jsonData = System.IO.File.ReadAllText(path);
            BindingList<Medicines>? jsonMedicines = JsonSerializer.Deserialize<BindingList<Medicines>>(jsonData);
            if (jsonMedicines != null)
            {
                this.medicinesList = jsonMedicines;
            }
        }

        private int findMedicines(String medicinesId)
        {
            int index = 0;
            foreach (Medicines medicines in medicinesList)
            {
                if (medicines.ID == medicinesId)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public Medicines? GetMedicines(String medicinesID)
        {
            foreach (var medicines in medicinesList)
            {
                if (medicines.ID == medicinesID)
                {
                    return medicines;
                }
            }
            return null;
        }

        public BindingList<Medicines> GetAllMedicines()
        {
            return medicinesList;
        }

        public Medicines? SetMedicines(Medicines medicines)
        {
            int index = findMedicines(medicines.ID);
            if (index == -1)
            {
                return null;
            }
            medicinesList.RemoveAt(index);
            medicinesList.Insert(index, medicines);
            writeMedicines();
            return medicines;
        }

        public Medicines CreateNewMedicines(Medicines medicines)
        {
            medicinesList.Add(medicines);
            writeMedicines();
            return medicines;
        }

        public void DeleteMedicines(String medicinesID)
        {
            foreach (var medicines in medicinesList)
            {
                if (medicines.ID == medicinesID)
                {
                    medicinesList.Remove(medicines);
                    break;
                }
            }
            this.writeMedicines();
        }
        private void writeMedicines()
        {
            String jsonString = JsonSerializer.Serialize(medicinesList);
            System.IO.File.WriteAllText(path, jsonString);
        }
    }
}
