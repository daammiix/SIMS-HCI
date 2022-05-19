using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Helpers
{
    public class QuantifiedMedicine
    {
        public Medicines Medicines { get; set; }
        public int Quantity { get; set; }

        public QuantifiedMedicine(Medicines m, int q)
        {
            Medicines = m;
            Quantity = q;
        }
    }
}
