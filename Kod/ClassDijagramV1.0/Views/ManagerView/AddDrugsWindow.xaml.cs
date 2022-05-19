using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for AddDrugsWindow.xaml
    /// </summary>
    public partial class AddDrugsWindow : Window
    {
        public RoomController roomController;
        public MedicineController medicineController;
        public Storage storage;
        private IRefreshableMedicineView medicineView;
        QuantifiedMedicine quantifiedMedicine;
        public BindingList<Medicines> MedicinesList { get; set; }
        public BindingList<String> SuggestedMedicines { get; set; }
        public BindingList<String> SelectedMedicines { get; set; }
        public BindingList<String> MedicineComponents { get; set; }
        public AddDrugsWindow(IRefreshableMedicineView medicineView)
        {
            InitializeComponent();
            var app = Application.Current as App;
            roomController = app.roomController;
            medicineController = app.medicinesController;
            this.medicineView = medicineView;
            this.quantifiedMedicine = (QuantifiedMedicine)quantifiedMedicine;
            storage = (Storage)roomController.GetRoom("storage");
            this.MedicinesList = medicineController.GetAllMedicines();
            this.SuggestedMedicines = new BindingList<String>();
            this.SelectedMedicines = new BindingList<String>();
            this.MedicineComponents = new BindingList<String>();

            this.DataContext = this;
        }

        private Medicines MedicineFromTextBoxes()
        {
            var addComponents = AddComponents.Text;
            string[] components = addComponents.Split(',');
            foreach (string c in components)
            {
                var component = c.Trim();
                MedicineComponents.Add(component);
            }
            return new Medicines(AddDrugsId.Text, AddDrugsName.Text, "Na čekanju", MedicineComponents, SuggestedMedicines);
        }

        private void SaveAddedDrugs_Click(object sender, RoutedEventArgs e)
        {
            var medicine = MedicineFromTextBoxes();
            var quantity = Int32.Parse(AddDrugsQuantity.Text);
            medicineController.AddMedicines(medicine);
            ((Storage)roomController.GetRoom("storage")).addNewMedicine(medicine, quantity);
            medicineView.RefreshMedicines();
            this.Close();
        }

        private void QuitAddedDrugs_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddSuggestedMedicine_Click(object sender, RoutedEventArgs e)
        {
            Medicines medicine = (Medicines)SuggestedMedicinesComboBox.SelectedItem;
            SuggestedMedicines.Add(medicine.ID);
            SelectedMedicines.Add(medicine.ID + " " + medicine.Name);
        }
    }
}
