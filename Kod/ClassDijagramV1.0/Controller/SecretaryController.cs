using ClassDijagramV1._0.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Controller
{
    public class SecretaryController
    {
        #region Fields

        private SecretaryService _secretaryService;

        #endregion

        #region Constructor

        public SecretaryController(SecretaryService ss)
        {
            _secretaryService = ss;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Vraca sve sekretare iz baze(fajla)
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Secretary> GetSecretaries()
        {
            return _secretaryService.GetSecretaries();
        }

        /// <summary>
        /// Cuva sve sekretare u bazu(fajl)
        /// </summary>
        public void SaveSecretaries()
        {
            _secretaryService.SaveSecretaries();
        }

        /// <summary>
        /// Dodaje novog sekretara 
        /// </summary>
        /// <param name="newSecretary"></param>
        public void AddSecretary(Secretary newSecretary)
        {
            _secretaryService.AddSecretary(newSecretary);
        }

        /// <summary>
        /// Vraca sekretara sa zadatim id-em ako postoji u suprnotom vraca null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Secretary GetSecretaryById(int id)
        {
            return _secretaryService.GetSecretaryById(id);
        }

        #endregion
    }
}
