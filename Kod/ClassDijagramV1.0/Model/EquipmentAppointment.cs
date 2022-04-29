using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Model
{
    public class EquipmentAppointment
    {
        public Room RoomFrom { get; set; }
        public Room RoomTo { get; set; }
        public Equipment SelectedEquipment { get; set; }
        public int Quantity { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }

        public EquipmentAppointment(Room RoomFrom, Room RoomTo, Equipment SelectedEquipment, int Quantity, DateTime FromDateTime, DateTime ToDateTime)
        {
            this.RoomFrom = RoomFrom;
            this.RoomTo = RoomTo;
            this.SelectedEquipment = SelectedEquipment;
            this.Quantity = Quantity;
            this.FromDateTime = FromDateTime;
            this.ToDateTime = ToDateTime;
        }
    }
}
