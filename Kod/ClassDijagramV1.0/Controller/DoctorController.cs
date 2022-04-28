// File:    DoctorController.cs
// Author:  x
// Created: Sunday, April 10, 2022 11:58:28 AM
// Purpose: Definition of Class DoctorController

using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Controller
{
   public class DoctorController
   {
        private DoctorService _doctorService;

        public DoctorController(DoctorService service)
        {
            _doctorService = service;
        }
        public void AddDoctor(Model.Doctor doctor)
      {
         throw new NotImplementedException();
      }
      
      public void RemoveDoctor(Model.Doctor doctor)
      {
         throw new NotImplementedException();
      }
      
      public void UpdateDoctor(Model.Doctor doctor)
      {
         throw new NotImplementedException();
      }
     
      
      public Model.Doctor GetOneDoctor(Model.Doctor doctor)
      {
         throw new NotImplementedException();
      }



        public ObservableCollection<Doctor> GetAllDoctors()
        {
            return _doctorService.GetAllDoctors();
        }

        public void SaveDoctors()
        {
            _doctorService.SaveDoctors();
        }

    }
}