using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class PurchaseOrder
    {
        public static int idCounter = 0;

        #region Properties

        public int Id { get; set; }
        public string SupplierName { get; set; }
        public List<PurchaseOrderEquipment> Equipment { get; set; }
        public string Description { get; set; }
        public double Price { get; set; } // postavimo je kasnije jer nemamo informacije ovde
        public DateTime DeliveryTime { get; set; }

        #endregion

        #region Constructor

        public PurchaseOrder(string supplierName, List<PurchaseOrderEquipment> equipment, string description,
            DateTime deliveryDate)
        {
            Id = ++idCounter;
            SupplierName = supplierName;
            Equipment = equipment;
            Description = description;
            DeliveryTime = deliveryDate;
        }

        public PurchaseOrder()
        {

        }

        #endregion
    }
}
