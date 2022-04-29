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
    public class ManagerService
    {
        #region Fields

        private ManagerRepo _managerRepo;

        #endregion

        #region Constructor

        public ManagerService(ManagerRepo mr)
        {
            _managerRepo = mr;
        }

        #endregion

        #region Methods

        public ObservableCollection<Manager> GetManagers()
        {
            return _managerRepo.GetManagers();
        }

        public void SaveManagers()
        {
            _managerRepo.SaveManagers();
        }

        /// <summary>
        /// Dodaje novog menadzera ako ne postoji menadzer sa istim id-em ili jmbg-om, u suprotnom updatuje postojeceg
        /// </summary>
        /// <param name="newManager"></param>
        public void AddManager(Manager newManager)
        {
            _managerRepo.AddManager(newManager);
        }

        /// <summary>
        /// Vraca menadzera sa zadatim id-em u suprotnom vraca null
        /// </summary>
        /// <param name="id"></param>
        public Manager GetManagerById(int id)
        {
            Manager? ret = null;
            foreach (var manager in _managerRepo.GetManagers())
            {
                if (manager.Id == id)
                {
                    ret = manager;
                    break;
                }
            }

            return ret;
        }

        #endregion
    }
}
