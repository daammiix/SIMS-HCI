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
    /// Interaction logic for ChangeRejectedDrugsWindow.xaml
    /// </summary>
    public partial class ChangeRejectedDrugsWindow : Window
    {
        public RoomController roomController;
        public MedicineController medicineController;
        public Storage storage;
        private IRefreshableMedicineView medicineView;
        QuantifiedMedicine quantifiedMedicine;
        public BindingList<String> MedicineComponents { get; set; }
        public ChangeRejectedDrugsWindow(QuantifiedMedicine? quantifiedMedicine, IRefreshableMedicineView medicineView)
        {
            InitializeComponent();
            var app = Application.Current as App;
            roomController = app.roomController;
            medicineController = app.medicinesController;
            this.medicineView = medicineView;
            this.quantifiedMedicine = (QuantifiedMedicine)quantifiedMedicine;
            storage = (Storage)roomController.GetRoom("storage");
            this.MedicineComponents = quantifiedMedicine.Medicines.MedicineComponents;

            ChangeComponents.Clear();

            foreach (var component in MedicineComponents)
            {
                ChangeComponents.AppendText(component + Environment.NewLine);
            }

            this.DataContext = quantifiedMedicine;
        }

        private void SaveChangedRejectedDrugs_Click(object sender, RoutedEventArgs e)
        {
            var medicine = MedicineFromTextBoxes();
            var quantity = Int32.Parse(ChangedDrugsQuantity.Text);
            String medicineId = quantifiedMedicine.Medicines.ID;

            medicineController.ChangeMedicines(medicine);
            roomController.ChangeStorageMedicineQuantity(medicineId, quantity);

            medicineView.RefreshMedicines();
            this.Close();
        }

        private void QuitChangedRejectedDrugs_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private Medicines MedicineFromTextBoxes()
        {
            var addComponents = ChangeComponents.Text;
            MedicineComponents.Clear();
            string[] components = addComponents.Split(',');
            foreach (string c in components)
            {
                var component = c.Trim();
                MedicineComponents.Add(component);
            }
            return new Medicines(ChangedDrugsId.Text, ChangedDrugsName.Text, ChangeDrugsStatus.Text, MedicineComponents);
        }
    }
}
