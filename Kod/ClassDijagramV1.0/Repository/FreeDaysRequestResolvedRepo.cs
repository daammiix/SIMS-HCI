using ClassDijagramV1._0.FileHandlers;
using ClassDijagramV1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class FreeDaysRequestResolvedRepo
    {
        #region Fields

        private FileHandler<FreeDayRequestResolved> _fileHandler;

        #endregion

        #region Properties

        public List<FreeDayRequestResolved> FreeDayRequestsResolved { get; set; }

        #endregion

        #region Constructor

        public FreeDaysRequestResolvedRepo(FileHandler<FreeDayRequestResolved> fileHandler)
        {
            _fileHandler = fileHandler;

            FreeDayRequestsResolved = _fileHandler.GetItems();
        }

        #endregion

        #region Methods

        public void SaveFreeDayRequests()
        {
            _fileHandler.SaveItems(FreeDayRequestsResolved);
        }

        public List<FreeDayRequestResolved> GetFreeDayRequests()
        {
            return FreeDayRequestsResolved;
        }

        public void AddNew(FreeDayRequestResolved freeDayRequestResolved)
        {
            FreeDayRequestsResolved.Add(freeDayRequestResolved);
        }

        /// <summary>
        /// Brise prosledjen request i vraca ga, ako ne postoji vraca null
        /// </summary>
        /// <param name="freeDayRequestResolvedToRemove"></param>
        /// <returns></returns>
        public FreeDayRequestResolved? Remove(FreeDayRequestResolved freeDayRequestResolvedToRemove)
        {
            if (FreeDayRequestsResolved.Remove(freeDayRequestResolvedToRemove))
            {
                return freeDayRequestResolvedToRemove;
            }

            return null;
        }

        #endregion

    }
}
