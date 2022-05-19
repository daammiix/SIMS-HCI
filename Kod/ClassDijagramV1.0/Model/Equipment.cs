using ClassDijagramV1._0.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class Equipment
    {
        public String EquipmentID { get; set; }
        public String Name { get; set; }
        public String EquipmentType { get; set; }
        public double UnitPrice { get; set; }
        public UnitsType Units { get; set; }

        public Equipment(String EquipmentID, String Name, String EquipmentType, double price, UnitsType units)
        {
            this.EquipmentID = EquipmentID;
            this.Name = Name;
            this.EquipmentType = EquipmentType;
            this.UnitPrice = price;
            this.Units = units;
        }

        public Equipment()
        {

        }
    }
}
