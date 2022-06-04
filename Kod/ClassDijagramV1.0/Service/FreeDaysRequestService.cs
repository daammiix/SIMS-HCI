using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Service
{
    public class FreeDaysRequestService
    {
        #region Fields

        private FreeDaysRequestRepo _freeDaysRequestRepo;

        #endregion

        #region Constructor

        public FreeDaysRequestService(FreeDaysRequestRepo freeDaysRequestRepo)
        {
            _freeDaysRequestRepo = freeDaysRequestRepo;
        }

        #endregion

        #region Methods

        public List<FreeDayRequest> GetFreeDayRequests()
        {
            return _freeDaysRequestRepo.GetFreeDayRequests();
        }

        public void SaveFreeDayRequests()
        {
            _freeDaysRequestRepo.SaveFreeDayRequests();
        }

        /// <summary>
        /// Brise freeDayRequest i vraca ga ako postoji, u suprotnom vraca null
        /// </summary>
        /// <param name="freeDayRequest"></param>
        /// <returns></returns>
        public FreeDayRequest? Remove(FreeDayRequest freeDayRequest)
        {
            return _freeDaysRequestRepo.Remove(freeDayRequest);
        }

        #endregion
    }
}
