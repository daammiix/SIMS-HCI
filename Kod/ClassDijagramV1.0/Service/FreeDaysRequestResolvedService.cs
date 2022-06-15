using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Service
{
    public class FreeDaysRequestResolvedService
    {
        #region Fields

        private FreeDaysRequestResolvedRepo _freeDaysRequestResolvedRepo;

        #endregion

        #region Constructor

        public FreeDaysRequestResolvedService(FreeDaysRequestResolvedRepo freeDaysRequestResolvedRepo)
        {
            _freeDaysRequestResolvedRepo = freeDaysRequestResolvedRepo;
        }

        #endregion

        #region Methods

        public List<FreeDayRequestResolved> GetFreeDayRequests()
        {
            return _freeDaysRequestResolvedRepo.GetFreeDayRequests();
        }

        public void SaveFreeDayRequests()
        {
            _freeDaysRequestResolvedRepo.SaveFreeDayRequests();
        }

        public void AddNew(FreeDayRequestResolved freeDayRequestResolved)
        {
            _freeDaysRequestResolvedRepo.AddNew(freeDayRequestResolved);
        }

        /// <summary>
        /// Brise prosledjeni request i vraca ga, ako ne postoji vraca null
        /// </summary>
        /// <param name="freeDayRequestResolved"></param>
        /// <returns></returns>
        public FreeDayRequestResolved? Remove(FreeDayRequestResolved freeDayRequestResolved)
        {
            return _freeDaysRequestResolvedRepo.Remove(freeDayRequestResolved);
        }

        #endregion

    }
}
