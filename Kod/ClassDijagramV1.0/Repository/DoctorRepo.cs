// File:    DoctorRepo.cs
// Author:  x
// Created: Sunday, April 10, 2022 11:29:59 AM
// Purpose: Definition of Class DoctorRepo

using ClassDijagramV1._0.FileHandlers;
using Model;
using System;
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
            //Doctors = new ObservableCollection<Doctor>();
            DateTime date1 = new DateTime(2008, 5, 1, 8, 30, 52);

            Doctor d1 = new Doctor("doktor", "doktor", "123", "musko", "3875432", "the292200", date1, DoctorType.general, null);
            Doctor d2 = new Doctor("lekar", "lekar", "123", "musko", "3875432", "the292200", date1, DoctorType.general, null);
            //Doctors.Add(d1);
            //Doctors.Add(d2);
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


    }
}