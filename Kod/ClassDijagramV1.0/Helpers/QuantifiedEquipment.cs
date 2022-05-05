using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Helpers
{
    public class QuantifiedEquipment
    {
        public Equipment Equipment { get; set; }
        public int Quantity { get; set; }

        public QuantifiedEquipment(Equipment e, int q)
        {
            Equipment = e;
            Quantity = q;
        }
    }
}
