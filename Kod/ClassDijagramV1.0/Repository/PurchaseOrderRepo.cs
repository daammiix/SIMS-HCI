using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class PurchaseOrderRepo
    {
        #region Fields

        private FileHandler<PurchaseOrder> _fileHandler;

        private List<PurchaseOrder> _purchaseOrders;

        #endregion

        #region Constructor

        public PurchaseOrderRepo()
        {
            // napravimo fileHandler
            _fileHandler = new FileHandler<PurchaseOrder>(App._purchaseOrdersFilePath);

            // ucitamo sve narudzbine iz fajla
            _purchaseOrders = _fileHandler.GetItems();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Vraca sve narudzbine
        /// </summary>
        /// <returns></returns>
        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return _purchaseOrders;
        }

        /// <summary>
        /// Cuva narudzbine u fajl
        /// </summary>
        public void SavePurchaseOrders()
        {
            _fileHandler.SaveItems(_purchaseOrders);
        }

        /// <summary>
        /// Dodaje novu narudzbinu
        /// </summary>
        /// <param name="newPurchaseOrder"></param>
        public void AddPurchaseOrder(PurchaseOrder newPurchaseOrder)
        {
            _purchaseOrders.Add(newPurchaseOrder);
        }

        /// <summary>
        /// Brise narudzbinu sa prosledjeni id-em
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Izbrisani purchase order ili null ako ne postoji PurchaseOrder sa zadatim id-em</returns>
        public PurchaseOrder RemovePurchaseOrder(int id)
        {
            PurchaseOrder? purchaseOrderToRemove = _purchaseOrders.Where(order => order.Id == id).FirstOrDefault();
            if (purchaseOrderToRemove != null)
            {
                _purchaseOrders.Remove(purchaseOrderToRemove);
            }

            return purchaseOrderToRemove;
        }

        #endregion
    }
}
