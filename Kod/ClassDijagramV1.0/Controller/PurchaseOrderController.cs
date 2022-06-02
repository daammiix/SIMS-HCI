using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System.Collections.Generic;

namespace ClassDijagramV1._0.Controller
{
    public class PurchaseOrderController
    {
        #region Fields

        private PurchaseOrderService _purchaseOrderService;

        #endregion

        #region Constructor

        public PurchaseOrderController(PurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Vraca sve narudzbine
        /// </summary>
        /// <returns></returns>
        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return _purchaseOrderService.GetPurchaseOrders();
        }

        /// <summary>
        /// Cuva sve narudzbine
        /// </summary>
        public void SavePurchaseOrders()
        {
            _purchaseOrderService.SavePurchaseOrders();
        }

        /// <summary>
        /// Dodaje novu narudzbinu
        /// </summary>
        /// <param name="newPurchaseOrder">Nova narudzbina</param>
        public void AddPurchaseOrder(PurchaseOrder newPurchaseOrder)
        {
            _purchaseOrderService.AddPurchaseOrder(newPurchaseOrder);
        }

        /// <summary>
        /// Brise narudzbinu sa prosledjenim id-em
        /// </summary>
        /// <param name="id">Id narudzbine za brisanje</param>
        /// <returns>Obrisanu narudzbinu ili null ako narudzbina sa prosledjenim id-em nije postojala</returns>
        public PurchaseOrder RemovePurchaseOrder(int id)
        {
            return _purchaseOrderService.RemovePurchaseOrder(id);
        }

        /// <summary>
        /// Premesta opremu iz narudzbine u magacin
        /// </summary>
        /// <param name="purchaseOrder"></param>
        public void MoveEquipmentToStorage(PurchaseOrder purchaseOrder)
        {
            _purchaseOrderService.MoveEquipmentToStorage(purchaseOrder);
        }

        #endregion
    }
}
