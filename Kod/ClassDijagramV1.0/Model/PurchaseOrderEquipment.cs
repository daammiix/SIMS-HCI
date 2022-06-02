namespace ClassDijagramV1._0.Model
{
    // Wrapuje equipment, dodaje mu kolicinu
    public class PurchaseOrderEquipment
    {
        #region Properties

        // Id equipmenta na koji se odnosi stavka u porudzbini
        public string EquipmentId { get; set; }
        public int Quantity { get; set; }

        #endregion

        #region Constructor

        public PurchaseOrderEquipment(string equipmentId, int quantity)
        {
            EquipmentId = equipmentId;
            Quantity = quantity;
        }

        #endregion
    }
}
