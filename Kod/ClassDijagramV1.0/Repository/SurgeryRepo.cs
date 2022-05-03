// File:    SurgeryRepo.cs
// Author:  x
// Created: Sunday, April 10, 2022 5:08:59 PM
// Purpose: Definition of Class SurgeryRepo

using ClassDijagramV1._0.Model;
using Model;
using System;
using System.Collections.ObjectModel;

namespace Repository
{
    public class SurgeryRepo
    {
        private string filePath;

        public ObservableCollection<Surgery> Surgeries;

        public SurgeryRepo()
        {
            Surgeries = new ObservableCollection<Surgery>();

            Room r1 = new Room();
            DateTime date1 = new DateTime(2008, 5, 1, 8, 30, 52);
            DateTime date2 = new DateTime(2010, 8, 18, 13, 30, 30);
            TimeSpan interval = date2 - date1;
            Doctor d1 = new Doctor("doktor", "doktor", "123", "musko", "3875432", "the292200", date1, new Address("Srbija", "Novi Sad", "Narodnog Fronta", "20"), DoctorType.general, null);


            Patient p1 = new Patient("djordje", "lipovcic", "123", "musko", "3875432", "the292200", date1, new Address("Srbija", "Novi Sad", "Atinska", "55"), "1234");
            Patient p2 = new Patient("sandra", "brkic", "123", "musko", "3875432", "the292200", date1, new Address("Srbija", "Novi Sad", "Carinska", "189"), "1234");



            Surgery s = new Surgery("54", date1, interval, p1, r1, d1);
            Surgeries.Add(s);
        }


        public ObservableCollection<Surgery> GetAllSurgeries()
        {
            return Surgeries;
        }

        public Surgery SetSurgery(Model.Surgery surgery)
        {
            throw new NotImplementedException();
        }

        public Model.Surgery AddNewSurgery(Model.Surgery newSurgery)
        {
            throw new NotImplementedException();
        }

        public void RemoveSurgery(String parameter1)
        {
            throw new NotImplementedException();
        }

    }
}