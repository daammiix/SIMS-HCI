// File:    SurgeryService.cs
// Author:  x
// Created: Sunday, April 10, 2022 5:12:07 PM
// Purpose: Definition of Class SurgeryService

using Model;
using Repository;
using System;
using System.Collections.ObjectModel;

namespace Service
{
    public class SurgeryService
    {
        private SurgeryRepo _surgeryRepo;

        public SurgeryService(SurgeryRepo surgeryRepo)
        {
            _surgeryRepo = surgeryRepo;
        }

        public void AddSurgery(Model.Surgery surgery)
        {
            throw new NotImplementedException();
        }

        public void RemoveSurgery(String surgeryID)
        {
            throw new NotImplementedException();
        }

        public void UpdateSurgery(Model.Surgery surgery)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Surgery> GetAllSurgeries()
        {
            return _surgeryRepo.GetAllSurgeries();
        }

        public Model.Surgery GetOneSurgery(String surgeryID)
        {
            throw new NotImplementedException();
        }

    }
}