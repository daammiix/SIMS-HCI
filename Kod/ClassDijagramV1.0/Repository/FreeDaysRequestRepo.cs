using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class FreeDaysRequestRepo
    {
        #region Fields

        private FileHandler<FreeDayRequest> _fileHandler;

        #endregion

        #region Properties

        public List<FreeDayRequest> FreeDayRequests { get; set; }

        #endregion

        #region Constructor

        public FreeDaysRequestRepo(FileHandler<FreeDayRequest> fileHandler)
        {
            _fileHandler = fileHandler;

            FreeDayRequests = _fileHandler.GetItems();

        }

        #endregion

        #region Methods 

        public void SaveFreeDayRequests()
        {
            _fileHandler.SaveItems(FreeDayRequests);
        }

        public List<FreeDayRequest> GetFreeDayRequests()
        {
            return FreeDayRequests;
        }

        /// <summary>
        /// Brise freeDayRequest iz liste i vraca ga ako postoji, u suprotnom vraca null
        /// </summary>
        /// <param name="freeDayRequest"></param>
        /// <returns></returns>
        public FreeDayRequest? Remove(FreeDayRequest freeDayRequest)
        {
            if (FreeDayRequests.Remove(freeDayRequest))
            {
                return freeDayRequest;
            }

            return null;
        }

        #endregion
    }
}
