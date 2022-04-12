// File:    SurgeryController.cs
// Author:  x
// Created: Sunday, April 10, 2022 4:55:03 PM
// Purpose: Definition of Class SurgeryController

using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Controller
{
   public class SurgeryController
   {
        private SurgeryService _surgeryService;

        public SurgeryController(SurgeryService surgeryService)
        {
           _surgeryService = surgeryService;
        }

        public void AddSurgery(Model.Surgery surgery)
      {
         throw new NotImplementedException();
      }
      
      public void RemoveSurgery(Model.Surgery surgery)
      {
         throw new NotImplementedException();
      }
      
      public void UpdateSurgery(Model.Surgery surgery)
      {
         throw new NotImplementedException();
      }
      
      public ObservableCollection<Surgery> GetAllSurgeries()
      {
            return _surgeryService.GetAllSurgeries();
      }
      
      public Surgery GetOneSurgery(Surgery surgery)
      {
         throw new NotImplementedException();
      }
   
   }
}