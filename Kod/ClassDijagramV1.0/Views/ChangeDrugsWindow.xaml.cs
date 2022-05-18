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

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for ChangeDrugsWindow.xaml
    /// </summary>
    public partial class ChangeDrugsWindow : Window
    {

        public RoomController roomController;
        public MedicineController medicineController;
        public Storage storage;
        private IRefreshableMedicineView medicineView;
        QuantifiedMedicine quantifiedMedicine;
        public ChangeDrugsWindow(QuantifiedMedicine? quantifiedMedicine, IRefreshableMedicineView medicineView)
        {
            InitializeComponent();
            var app = Application.Current as App;
            roomController = app.roomController;
            medicineController = app.medicinesController;
            this.medicineView = medicineView;
            this.quantifiedMedicine = (QuantifiedMedicine)quantifiedMedicine;
            storage = (Storage)roomController.GetRoom("storage");
            this.DataContext = quantifiedMedicine;
        }

        private Medicines MedicineFromTextBoxes()
        {
            return new Medicines(ChangedDrugsId.Text, ChangedDrugsName.Text, ChangeDrugsStatus.Text);
        }

        private void SaveChangedDrugs_Click(object sender, RoutedEventArgs e)
        {
            var medicine = MedicineFromTextBoxes();
            var quantity = Int32.Parse(ChangedDrugsQuantity.Text);
            String medicineId = quantifiedMedicine.Medicines.ID;

            medicineController.ChangeMedicines(medicine);
            roomController.ChangeStorageMedicineQuantity(medicineId, quantity);

            medicineView.RefreshMedicines();
            this.Close();
        }

        private void QuitChangedDrugs_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
