using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Util;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class WorkersViewModel : ObservableObject
    {
        public DoctorController doctorController;
        public ManagerController managerController;
        private BindingList<Person> _workers = new BindingList<Person>();
        private BindingList<Person> _allWorkers;

        String _searchText = "";

        public WorkersViewModel()
        {
            var app = Application.Current as App;
            this.doctorController = app.DoctorController;
            this.managerController = app.ManagerController;
            _allWorkers = GetAllWorkers();
            _allWorkers.ListChanged += _allWorkersChanged;
            RefreshWorkers();
        }

        public BindingList<Person> Workers
        {
            get
            {
                return _workers;
            }
            set
            {
                if (_workers == value)
                {
                    return;
                }
                _workers = value;
            }
        }

        public String SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_searchText == value)
                {
                    return;
                }
                _searchText = value;
                RefreshWorkers();
            }
        }

        private BindingList<Person> GetAllWorkers()
        {
            var _workersList = new BindingList<Person>();
            var doctorList = doctorController.GetAllDoctors();
            var managerList = managerController.GetManagers();

            foreach (var doctor in doctorList)
            {
                _workersList.Add(doctor);
            }
            foreach (var manager in managerList)
            {
                _workersList.Add(manager);
            }
            return _workersList;
        }

        public void RefreshWorkers()
        {
            Workers.Clear();
            foreach (Person worker in _allWorkers)
            {
                String search = _searchText.ToLower();
                if (
                    !worker.Name.ToLower().Contains(search) &&
                    !worker.Surname.ToLower().Contains(search) &&
                    !worker.DateOfBirth.ToString().Contains(search) &&
                    !worker.Email.ToString().Contains(search) &&
                    !worker.Address.City.ToString().Contains(search) &&
                    !worker.Address.StreetName.ToLower().Contains(search) &&
                    !worker.Address.StreetNumber.ToLower().Contains(search) &&
                    !worker.PhoneNumber.ToString().Contains(search))
                {
                    continue;
                }
                Workers.Add(worker);
            }
        }
        public void _allWorkersChanged(object? sender, ListChangedEventArgs e)
        {
            RefreshWorkers();
        }
    }
}
