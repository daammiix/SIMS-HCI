using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Converters;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for StorageDrugsView.xaml
    /// </summary>
    public partial class StorageDrugsView : UserControl, IRefreshableMedicineView
    {
        MedicineController medicinesController;
        Medicines drug;
        Storage storage;
        RoomController roomController;
        public BindingList<Medicines> AllMedicines { get; set; }
        public BindingList<QuantifiedMedicine> MedicinesList { get; set; }
        public StorageDrugsView()
        {
            InitializeComponent();
            var app = Application.Current as App;
            MedicinesList = new BindingList<QuantifiedMedicine>();
            medicinesController = app.medicinesController;
            roomController = app.roomController;
            AllMedicines = medicinesController.GetAllMedicines();
            storage = (Storage)roomController.GetRoom("storage");
            RefreshMedicines();

            this.DataContext = this;
        }

        private void AddDrugs_Click(object sender, RoutedEventArgs e)
        {
            AddDrugsWindow addDrugsWindow = new AddDrugsWindow(this);
            addDrugsWindow.Show();
        }

        private void ChangeDrugs_Click(object sender, RoutedEventArgs e)
        {
            QuantifiedMedicine? quantifiedMedicine = (QuantifiedMedicine?)DrugsListGrid.SelectedItem;
            if (quantifiedMedicine != null)
            {
                if (quantifiedMedicine.Medicines.Status.Equals("Odobren"))
                {
                    ChangeDrugsWindow changeDrugsWindow = new ChangeDrugsWindow(quantifiedMedicine, this);
                    changeDrugsWindow.Show();
                }else
                {
                    ChangeRejectedDrugsWindow changeRejectedDrugsWindow = new ChangeRejectedDrugsWindow(quantifiedMedicine, this);
                    changeRejectedDrugsWindow.Show();
                }
            }
            else
            {
                Warning warning = new Warning();
                warning.Show();
            }
        }

        private void DeleteDrugs_Click(object sender, RoutedEventArgs e)
        {
            QuantifiedMedicine? quantifiedMedicine = (QuantifiedMedicine?)DrugsListGrid.SelectedItem;
            if (quantifiedMedicine != null)
            {
                DeleteDrugsWindow deleteDrugsWindow = new DeleteDrugsWindow(quantifiedMedicine, this);
                deleteDrugsWindow.Show();
            }
            else
            {
                Warning warning = new Warning();
                warning.Show();
            }
        }

        public void RefreshMedicines()
        {
            MedicinesList.Clear();
            foreach (var binding in storage.MedicineList)
            {
                foreach (var medicine in AllMedicines)
                {
                    if (medicine.ID == binding.MedicineID)
                    {
                        MedicinesList.Add(new QuantifiedMedicine(medicine, binding.Quantity));
                    }
                }
            }
        }

        private void OnOpenComponents_Click(object sender, RoutedEventArgs e)
        {
            QuantifiedMedicine? quantifiedMedicine = (QuantifiedMedicine?)DrugsListGrid.SelectedItem;
            MedicineComponentsWindow medicineComponents = new MedicineComponentsWindow(quantifiedMedicine);
            medicineComponents.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txb = sender as TextBox;
            if (txb.Text != "")
            {
                var filteredList = MedicinesList.Where(r => (r.Medicines.ID.ToLower().Contains(txb.Text.ToLower()) || r.Medicines.Name.ToLower().Contains(txb.Text.ToLower()) || r.Medicines.Status.ToLower().Contains(txb.Text.ToLower()) || r.Quantity.ToString().Contains(txb.Text)));
                DrugsListGrid.ItemsSource = null;
                DrugsListGrid.ItemsSource = filteredList;
            }
            else
            {
                DrugsListGrid.ItemsSource = MedicinesList;
            }
        }
    }
}
