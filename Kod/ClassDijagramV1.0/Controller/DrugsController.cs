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
    public class DrugsController
    {
        private DrugsService drugsService;

        public DrugsController(DrugsService drugsService)
        {
            this.drugsService = drugsService;
        }

        public void AddDrugs(Drugs drugs)
        {
            drugsService.AddDrugs(drugs);
        }

        public void DeleteDrugs(String drugsID)
        {
            drugsService.DeleteDrugs(drugsID);
        }

        public void ChangeDrugs(Drugs drugs)
        {
            drugsService.ChangeDrugs(drugs);
        }

        public BindingList<Drugs> GetAllDrugs()
        {
            return drugsService.GetAllDrugs();
        }

        public Drugs? GetDrugsByID(String DrugsID)
        {
            return drugsService.GetADrugs(DrugsID);
        }
    }
}
