using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using Model;
using Service;
using System.Collections.Generic;

namespace ClassDijagramV1._0.Service
{
    public class PurchaseOrderService
    {
        #region Fields

        private PurchaseOrderRepo _purchaseOrderRepo;

        private RoomService _roomService;

        private EquipmentService _equipmentService;

        #endregion

        #region Constructor

        public PurchaseOrderService(PurchaseOrderRepo purchaseOrderRepo, RoomService roomService, EquipmentService equipmentService)
        {
            _purchaseOrderRepo = purchaseOrderRepo;
            _roomService = roomService;
            _equipmentService = equipmentService;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Vraca sve narudzbine
        /// </summary>
        /// <returns></returns>
        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return _purchaseOrderRepo.GetPurchaseOrders();
        }

        /// <summary>
        /// Cuva sve narudzbine
        /// </summary>
        public void SavePurchaseOrders()
        {
            _purchaseOrderRepo.SavePurchaseOrders();
        }

        /// <summary>
        /// Dodaje novu narudzbinu
        /// </summary>
        /// <param name="newPurchaseOrder">Nova narudzbina</param>
        public void AddPurchaseOrder(PurchaseOrder newPurchaseOrder)
        {
            _purchaseOrderRepo.AddPurchaseOrder(newPurchaseOrder);
        }

        /// <summary>
        /// Brise narudzbinu sa prosledjenim id-em
        /// </summary>
        /// <param name="id">Id narudzbine za brisanje</param>
        /// <returns>Obrisanu narudzbinu ili null ako narudzbina sa prosledjenim id-em nije postojala</returns>
        public PurchaseOrder RemovePurchaseOrder(int id)
        {
            return _purchaseOrderRepo.RemovePurchaseOrder(id);
        }


        /// <summary>
        /// Premesta opremu iz narudzbine u magacin
        /// </summary>
        /// <param name="purchaseOrder"></param>
        public void MoveEquipmentToStorage(PurchaseOrder purchaseOrder)
        {
            Room storage = _roomService.GetARoom("storage");

            if (purchaseOrder != null)
            {
                foreach (PurchaseOrderEquipment purchaseOrderEquipment in purchaseOrder.Equipment)
                {
                    // Uzmemo equipment
                    Equipment? equipment = _equipmentService.GetAEquipment(purchaseOrderEquipment.EquipmentId);
                    // Uzmemo kolicinu
                    int quantity = purchaseOrderEquipment.Quantity;

                    if (equipment != null)
                    {
                        // Ako je razlicit od null dodamo ga
                        storage.addEquipment(equipment, quantity);
                    }
                }
            }
        }

        #endregion
    }
}