using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Controller
{
    public class FreeDaysRequestResolvedController
    {
        #region Fields

        private FreeDaysRequestResolvedService _freeDaysRequestResolvedService;

        #endregion

        #region Constructor

        public FreeDaysRequestResolvedController(FreeDaysRequestResolvedService service)
        {
            _freeDaysRequestResolvedService = service;
        }

        #endregion

        #region Methods

        public List<FreeDayRequestResolved> GetFreeDayRequests()
        {
            return _freeDaysRequestResolvedService.GetFreeDayRequests();
        }

        public void SaveFreeDayRequests()
        {
            _freeDaysRequestResolvedService.SaveFreeDayRequests();
        }

        public void AddFreeDayRequestResolved(FreeDayRequestResolved freeDayRequestResolved)
        {
            _freeDaysRequestResolvedService.AddNew(freeDayRequestResolved);
        }

        /// <summary>
        /// Brise prosledjeni request i vraca ga, ako ne postoji vraca null
        /// </summary>
        /// <param name="freeDayRequestResolved"></param>
        /// <returns></returns>
        public FreeDayRequestResolved? Remove(FreeDayRequestResolved freeDayRequestResolved)
        {
            return _freeDaysRequestResolvedService.Remove(freeDayRequestResolved);
        }


        #endregion
    }
}
