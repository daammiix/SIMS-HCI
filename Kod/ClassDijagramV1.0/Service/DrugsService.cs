using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Service
{
    public class DrugsService
    {
        private DrugsRepo drugsRepo;
        public DrugsService(DrugsRepo drugsRepo)
        {
            this.drugsRepo = drugsRepo;
        }

        public void AddDrugs(Drugs drugs)
        {
            if (this.CheckIfUniq(drugs, false))
            {
                drugsRepo.CreateNewDrugs(drugs);
            }
        }

        public void DeleteDrugs(String drugsID)
        {
            drugsRepo.DeleteDrugs(drugsID);
        }

        public void ChangeDrugs(Drugs drugs)
        {
            if (this.CheckIfUniq(drugs, true))
            {
                drugsRepo.SetDrugs(drugs);
            }
        }

        public Drugs? GetADrugs(String drugsID)
        {
            return drugsRepo.GetDrugs(drugsID);
        }

        public BindingList<Drugs> GetAllDrugs()
        {
            return drugsRepo.GetAllDrugs();
        }

        public Boolean CheckIfUniq(Drugs drugs, bool existingDrugs)
        {
            var drugsList = drugsRepo.GetAllDrugs();
            foreach (var r in drugsList)
            {
                if (!existingDrugs && drugs.DrugsID == r.DrugsID)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
