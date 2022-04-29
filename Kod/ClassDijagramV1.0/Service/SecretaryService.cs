using ClassDijagramV1._0.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Service
{
    public class SecretaryService
    {
        #region Fields

        private SecretaryRepo _secretaryRepo;

        #endregion

        #region Constructor

        public SecretaryService(SecretaryRepo sr)
        {
            _secretaryRepo = sr;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Vraca sve sekretare iz baze(fajla)
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Secretary> GetSecretaries()
        {
            return _secretaryRepo.GetSecretaries();
        }

        /// <summary>
        /// Cuva sve sekretare u bazu(fajl)
        /// </summary>
        public void SaveSecretaries()
        {
            _secretaryRepo.SaveSecretaries();
        }

        /// <summary>
        /// Dodaje novog sekretara 
        /// </summary>
        /// <param name="newSecretary"></param>
        public void AddSecretary(Secretary newSecretary)
        {
            _secretaryRepo.AddSecretary(newSecretary);
        }

        /// <summary>
        /// Vraca sekretara sa zadatim id-em ako postoji u suprnotom vraca null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Secretary GetSecretaryById(int id)
        {
            Secretary? ret = null;

            foreach (Secretary sc in _secretaryRepo.GetSecretaries())
            {
                if (sc.Id == id)
                {
                    ret = sc;
                    break;
                }
            }

            return ret;
        }

        #endregion

    }
}
