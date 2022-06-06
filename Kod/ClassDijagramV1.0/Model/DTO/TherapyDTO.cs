using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model.DTO
{
    public class TherapyDTO
    {
        private String name;
        private DateTime date;
        public String Name { get { return name; } }
        public DateTime Date { get { return date; } }

        public TherapyDTO(string name, DateTime date)
        {
            this.name = name;
            this.date = date;
        }

    }
}
