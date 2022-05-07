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
    public class DrugsRepo
    {
        private String path = "..\\..\\..\\Data\\drugs.json";
        private BindingList<Drugs> drugsList = new BindingList<Drugs>();

        public DrugsRepo()
        {
            string jsonData = System.IO.File.ReadAllText(path);
            BindingList<Drugs>? jsonDrugs = JsonSerializer.Deserialize<BindingList<Drugs>>(jsonData);
            if (jsonDrugs != null)
            {
                this.drugsList = jsonDrugs;
            }
        }

        private int findDrugs(String drugsId)
        {
            int i = 0;
            foreach (Drugs drugs in drugsList)
            {
                if (drugs.DrugsID == drugsId)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public Drugs? GetDrugs(String drugsID)
        {
            foreach (var drugs in drugsList)
            {
                if (drugs.DrugsID == drugsID)
                {
                    return drugs;
                }
            }
            return null;
        }

        public BindingList<Drugs> GetAllDrugs()
        {
            return drugsList;
        }

        public Drugs? SetDrugs(Drugs drugs)
        {
            int i = findDrugs(drugs.DrugsID);
            if (i == -1)
            {
                return null;
            }
            drugsList.RemoveAt(i);
            drugsList.Insert(i, drugs);
            writeDrugs();
            return drugs;
        }

        public Drugs CreateNewDrugs(Drugs drugs)
        {
            drugsList.Add(drugs);
            writeDrugs();
            return drugs;
        }

        public void DeleteDrugs(String drugsID)
        {
            foreach (var drugs in drugsList)
            {
                if (drugs.DrugsID == drugsID)
                {
                    drugsList.Remove(drugs);
                    break;
                }
            }
            this.writeDrugs();
        }
        private void writeDrugs()  // TODO: Add to class diagram
        {
            String jsonString = JsonSerializer.Serialize(drugsList);
            System.IO.File.WriteAllText(path, jsonString);
        }
    }
}
