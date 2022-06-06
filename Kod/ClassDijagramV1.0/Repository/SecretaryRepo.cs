using ClassDijagramV1._0.FileHandlers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public class SecretaryRepo
    {
        #region Fields

        private FileHandler<Secretary> _fileHandler;

        #endregion

        #region Properties

        public ObservableCollection<Secretary> Secretaries { get; set; }


        #endregion

        #region Constructor

        public SecretaryRepo(FileHandler<Secretary> fileHandler)
        {
            _fileHandler = fileHandler;

            Secretaries = new ObservableCollection<Secretary>(_fileHandler.GetItems());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Vraca sve sekretare iz baze(fajla)
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Secretary> GetSecretaries()
        {
            return Secretaries;
        }

        /// <summary>
        /// Cuva sve sekretare u bazu(fajl)
        /// </summary>
        public void SaveSecretaries()
        {
            _fileHandler.SaveItems(Secretaries.ToList());
        }

        /// <summary>
        /// Dodaje novog sekretara ako ne postoji sekretar sa istim id-em i jmbg-om, u suprotnom pregazimo starog
        /// </summary>
        /// <param name="newSecretary"></param>
        public void AddSecretary(Secretary newSecretary)
        {
            bool exists = false;
            Secretary? toUpdate = null;
            foreach (Secretary secretary in Secretaries)
            {
                if (secretary.Id == newSecretary.Id || secretary.Jmbg == newSecretary.Jmbg)
                {
                    toUpdate = secretary;
                    exists = true;
                    break;
                }
            }
            if (toUpdate != null)
            {
                toUpdate = newSecretary;
            }
            if (!exists)
            {
                Secretaries.Add(newSecretary);
            }
        }

        #endregion
    }
}
