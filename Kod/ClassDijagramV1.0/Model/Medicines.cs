using ClassDijagramV1._0.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class Medicines
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Status { get; set; }
        public BindingList<String> MedicineComponents { get; set; } = new BindingList<String>();
        public BindingList<String> SuggestedMedicines { get; set; } = new BindingList<String>();

        public Medicines(String ID, String Name, String Status, BindingList<String>? MedicineComponents = null, BindingList<String>? SuggestedMedicines = null)
        {
            this.ID = ID;
            this.Name = Name;
            this.Status = Status;
            if (MedicineComponents != null) { this.MedicineComponents = MedicineComponents; }
            if (SuggestedMedicines != null) { this.SuggestedMedicines = SuggestedMedicines; }
        }
    }
}
