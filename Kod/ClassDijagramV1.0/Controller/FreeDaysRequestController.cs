using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Controller
{
    public class FreeDaysRequestController
    {
        #region Fields

        private FreeDaysRequestService _freeDaysRequestService;

        #endregion

        #region Constructor

        public FreeDaysRequestController(FreeDaysRequestService service)
        {
            _freeDaysRequestService = service;
        }

        #endregion

        #region Methods

        public List<FreeDayRequest> GetFreeDayRequests()
        {
            return _freeDaysRequestService.GetFreeDayRequests();
        }

        public void SaveFreeDayRequests()
        {
            _freeDaysRequestService.SaveFreeDayRequests();
        }

        /// <summary>
        /// Brise freeDayRequest i vraca ga, u slucaju da ne postoji vraca null
        /// </summary>
        /// <param name="freeDayRequest"></param>
        /// <returns></returns>
        public FreeDayRequest? Remove(FreeDayRequest freeDayRequest)
        {
            return _freeDaysRequestService.Remove(freeDayRequest);
        }

        #endregion
    }
}
