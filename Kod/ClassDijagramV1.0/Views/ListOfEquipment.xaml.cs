﻿using Model;
using System;
using Controller;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using ClassDijagramV1._0.Controller;
using System.ComponentModel;
using ClassDijagramV1._0.Model;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for ListOfEquipment.xaml
    /// </summary>
    public partial class ListOfEquipment : Window
    {
        public struct QuantifiedEquipment
        {
            public Equipment Equipment { get; set; }
            public int Quantity { get; set; }
        }

        EquipmentController equipmentController;
        public BindingList<QuantifiedEquipment> EquipmentList { get; set; }

        public ListOfEquipment(Room room)
        {
            InitializeComponent();
            var app = Application.Current as App;
            EquipmentList = new BindingList<QuantifiedEquipment>();

            equipmentController = app.equipmentController;
            BindingList<Equipment> allEquipment = equipmentController.GetAllEquipments();
            foreach (var binding in room.EquipmentList)
            {
                foreach (var e in allEquipment)
                {
                    if (e.EquipmentID == binding.EquipmentID)
                    {
                        EquipmentList.Add(new QuantifiedEquipment()
                        {
                            Equipment = e,
                            Quantity = binding.Quantity,
                        });
                    }
                }
            }
            this.DataContext = this;
        }
        public void CloseListOfEquipment_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    } 
}