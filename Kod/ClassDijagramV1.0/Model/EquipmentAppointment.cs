using System;

namespace ClassDijagramV1._0.Model
{
    public class EquipmentAppointment
    {
        public String RoomFrom { get; set; }
        public String RoomTo { get; set; }
        public Equipment SelectedEquipment { get; set; }
        public int Quantity { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public bool Started { get; set; }

        public EquipmentAppointment(String RoomFrom, String RoomTo, Equipment SelectedEquipment, int Quantity, DateTime FromDateTime, DateTime ToDateTime)
        {
            this.RoomFrom = RoomFrom;
            this.RoomTo = RoomTo;
            this.SelectedEquipment = SelectedEquipment;
            this.Quantity = Quantity;
            this.FromDateTime = FromDateTime;
            this.ToDateTime = ToDateTime;
            this.Started = false;
        }
    }
}
