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