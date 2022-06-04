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

            // TODO: test podaci
            // FreeDayRequest test1 = new FreeDayRequest(4, DateTime.Now, DateTime.Now.AddDays(3), "Putovanje");
            // FreeDayRequest test2 = new FreeDayRequest(5, DateTime.Now.AddDays(10), DateTime.Now.AddDays(20), "Bolovanje");
            // FreeDayRequest test3 = new FreeDayRequest(6, DateTime.Now.AddDays(30), DateTime.Now.AddDays(31), "Slavim slavu");
            // FreeDayRequests.Add(test1);
            // FreeDayRequests.Add(test2);
            // FreeDayRequests.Add(test3);
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
        /// Brise freeDayRequest iz liste i vraca ga ako postoji, u suprotnom vraca false
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
