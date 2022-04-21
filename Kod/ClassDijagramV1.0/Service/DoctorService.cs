// File:    DoctorService.cs
// Author:  x
// Created: Sunday, April 10, 2022 11:47:05 AM
// Purpose: Definition of Class DoctorService

using Model;
using Repository;
using System;
using System.Collections.ObjectModel;

namespace Service
{
   public class DoctorService
   {
        private DoctorRepo _doctorRepo;

        public DoctorService(DoctorRepo repo)
        {
            _doctorRepo = repo;
        }
        public void AddDoctor(Model.Doctor doctor)
      {
         throw new NotImplementedException();
      }
      
      public void RemoveDoctor(String doctorID)
      {
         throw new NotImplementedException();
      }
      
      public void UpdateDoctor(Model.Doctor doctor)
      {
         throw new NotImplementedException();
      }

        public ObservableCollection<Doctor> GetAllDoctors() // obrisi iz bajdinga preglede koji mi ne trebaju, tj nisu od tog pacijenta
        {
            return _doctorRepo.GetDoctors();
        }


        public void SaveDoctors()
        {
            _doctorRepo.SaveDoctors();
        }

    }
}