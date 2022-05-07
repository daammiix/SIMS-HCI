using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class Drugs
    {
        public String DrugsID { get; set; }
        public String Name { get; set; }
        public String DrugsStatus { get; set; }

        public Drugs(String DrugsID, String Name, String DrugsStatus)
        {
            this.DrugsID = DrugsID;
            this.Name = Name;
            this.DrugsStatus = DrugsStatus;
        }
    }
}
