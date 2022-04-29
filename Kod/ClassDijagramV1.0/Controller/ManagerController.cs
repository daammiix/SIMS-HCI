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
    public class ManagerController
    {
        #region Fields

        ManagerService _managerService;

        #endregion

        #region Constructor
        public ManagerController(ManagerService ms)
        {
            _managerService = ms;
        }

        #endregion

        #region Methods

        public ObservableCollection<Manager> GetManagers()
        {
            return _managerService.GetManagers();
        }

        public void SaveManagers()
        {
            _managerService.SaveManagers();
        }

        /// <summary>
        /// Dodaje novog menadzera ako ne postoji menadzer sa istim id-em ili jmbg-om, u suprotnom updatuje postojeceg
        /// </summary>
        /// <param name="newManager"></param>
        public void AddManager(Manager newManager)
        {
            _managerService.AddManager(newManager);
        }

        /// <summary>
        /// Vraca menadzera sa zadatim id-em u suprotnom vraca null
        /// </summary>
        /// <param name="id"></param>
        public Manager GetManagerById(int id)
        {
            return _managerService.GetManagerById(id);
        }

        #endregion
    }
}
