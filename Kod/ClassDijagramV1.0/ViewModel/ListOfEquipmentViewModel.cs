using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.Views.ManagerView;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0.ViewModel
{
    public class ListOfEquipmentViewModel
    {
        EquipmentController equipmentController;
        public BindingList<QuantifiedEquipment> EquipmentList { get; set; }

        ListOfEquipment listOfEquipment;
        private RelayCommand _closeListOfEquipment;
        public ListOfEquipmentViewModel(ListOfEquipment listOfEquipment, Room room)
        {
            var app = Application.Current as App;
            EquipmentList = new BindingList<QuantifiedEquipment>();
            this.listOfEquipment = listOfEquipment;
            equipmentController = app.equipmentController;
            BindingList<Equipment> allEquipment = equipmentController.GetAllEquipments();
            foreach (var binding in room.EquipmentList)
            {
                foreach (var e in allEquipment)
                {
                    if (e.EquipmentID == binding.EquipmentID)
                    {
                        EquipmentList.Add(new QuantifiedEquipment(e, binding.Quantity));
                    }
                }
            }
        }

        public RelayCommand CloseListOfEquipment
        {
            get
            {
                _closeListOfEquipment = new RelayCommand(o =>
                {
                    listOfEquipment.Close();
                });

                return _closeListOfEquipment;
            }
        }
    }
}
