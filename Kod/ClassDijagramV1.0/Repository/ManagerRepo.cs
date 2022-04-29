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
    public class ManagerRepo
    {
        #region Fields

        private FileHandler<Manager> _fileHandler;

        #endregion

        #region Properties

        public ObservableCollection<Manager> Managers { get; set; }

        #endregion

        #region Constructor

        public ManagerRepo(FileHandler<Manager> fileHandler)
        {
            _fileHandler = fileHandler;

            Managers = new ObservableCollection<Manager>(fileHandler.GetItems());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Cuva sve menadzere u bazu(fajl)
        /// </summary>
        public void SaveManagers()
        {
            _fileHandler.SaveItems(Managers.ToList());
        }

        /// <summary>
        /// Vraca observable collection svih menadzere
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Manager> GetManagers()
        {
            return Managers;
        }

        /// <summary>
        /// Dodaje novog menadzera ako ne postoji menadzer sa istim id-em ili jmbg-om, u suprotnom updatuje postojeceg
        /// </summary>
        /// <param name="newManager"></param>
        public void AddManager(Manager newManager)
        {
            bool exists = false;
            Manager? toUpdate = null;
            foreach (Manager m in Managers)
            {
                if (m.Id == newManager.Id || m.Jmbg.Equals(newManager.Id))
                {
                    exists = true;
                    toUpdate = m;
                    break;
                }
            }

            if (!exists)
            {
                Managers.Add(newManager);
            }

            if (toUpdate != null)
            {
                toUpdate = newManager;
            }
        }

        #endregion
    }
}
