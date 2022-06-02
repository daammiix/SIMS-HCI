// File:    DoctorRepo.cs
// Author:  x
// Created: Sunday, April 10, 2022 11:29:59 AM
// Purpose: Definition of Class DoctorRepo

using ClassDijagramV1._0.FileHandlers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repository
{
    public class DoctorRepo
    {
        private String Path;
        private DoctorFileHandler _doctorFileHandler;
        public ObservableCollection<Doctor> Doctors;

        public DoctorRepo(DoctorFileHandler doctorFileHandler)
        {
            _doctorFileHandler = doctorFileHandler;
            Doctors = new ObservableCollection<Doctor>(_doctorFileHandler.GetDoctors());
        }

        public DoctorRepo()
        {
        }

        public ObservableCollection<Doctor> GetDoctors()
        {
            return Doctors;
        }

        public void SaveDoctors()
        {
            _doctorFileHandler.SaveDoctors(Doctors.ToList());
        }

        public Doctor GetDoctor(String doctorID)
        {
            throw new NotImplementedException();
        }

        public Doctor SetDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public void AddDoctor(Doctor newDoctor)
        {
            // Dodajemo novog doktora ako vec ne postoji doktor sa istim jmbg-om i idem, ako postoji pregazimo starog
            bool exists = false;
            Doctor? toUpdate = null;

            foreach (Doctor doctor in Doctors)
            {
                if (doctor.Id == newDoctor.Id || doctor.Jmbg.Equals(newDoctor.Jmbg))
                {
                    exists = true;
                    toUpdate = doctor;
                    break;
                }
            }
            if (toUpdate != null)
            {
                toUpdate = newDoctor;
            }
            if (!exists)
            {
                Doctors.Add(newDoctor);
            }
        }

        public void RemoveDoctor(String doctorID)
        {
            Doctors.Remove(FindDoctorById(doctorID));
        }


        public Doctor FindDoctorById(String doctorID)
        {
            foreach (Doctor a in Doctors)
            {
                if (a.Surname.Equals(doctorID))
                {
                    return a;
                }
            }
            return null;
        }

        /// <summary>
        /// Vraca sve doktore za zadatu specijalizaciju
        /// </summary>
        /// <param name="specialization"></param>
        /// <returns> Sve doktore za zadatu specijalizaciju  </returns>
        public List<Doctor> GetDoctorsForSpecialization(DoctorType specialization)
        {
            // Filtriramo sve doktore tako da uzmemo samo one sa prosledjenom specijalizacijom
            return Doctors.Where(doctor => doctor.Type == specialization).ToList();
        }

    }
}