// File:    DoctorService.cs
// Author:  x
// Created: Sunday, April 10, 2022 11:47:05 AM
// Purpose: Definition of Class DoctorService

using Model;
using Repository;
using System;
using System.Collections.Generic;
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
        public void AddDoctor(Doctor doctor)
        {
            _doctorRepo.AddDoctor(doctor);
        }

        public void RemoveDoctor(String doctorID)
        {
            throw new NotImplementedException();
        }

        public void UpdateDoctor(Doctor doctor)
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

        /// <summary>
        /// Vraca doktora sa zadatim id-em u suprotnom vraca null
        /// </summary>
        /// <param name="id"></param>
        public Doctor GetDoctorById(int id)
        {
            Doctor? ret = null;
            foreach (Doctor doctor in _doctorRepo.GetDoctors())
            {
                if (doctor.Id == id)
                {
                    ret = doctor;
                    break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Vraca sve doktore za zadatu specijalizaciju
        /// </summary>
        /// <param name="specialization"></param>
        /// <returns> Sve doktore za zadatu specijalizaciju ako nema doktora onda vraca praznu listu </returns>
        public List<Doctor> GetDoctorsForSpecialization(DoctorType specialization)
        {
            return _doctorRepo.GetDoctorsForSpecialization(specialization);
        }

    }
}