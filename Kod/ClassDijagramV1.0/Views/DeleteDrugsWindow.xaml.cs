using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using Controller;
using System;
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

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for DeleteDrugs.xaml
    /// </summary>
    public partial class DeleteDrugsWindow : Window
    {
        public RoomController roomController;
        public MedicineController medicineController;
        public Storage storage;
        private IRefreshableMedicineView medicineView;
        QuantifiedMedicine quantifiedMedicine;
        public DeleteDrugsWindow(QuantifiedMedicine? quantifiedMedicine, IRefreshableMedicineView medicineView)
        {
            InitializeComponent();
            var app = Application.Current as App;
            medicineController = app.medicinesController;
            roomController = app.roomController;
            this.medicineView = medicineView;
            this.quantifiedMedicine = (QuantifiedMedicine)quantifiedMedicine;
        }

        private void DeleteYesDrugs_Click(object sender, RoutedEventArgs e)
        {
            String medicineId = quantifiedMedicine.Medicines.ID;
            int index = findMedicine(medicineId);
            medicineController.DeleteMedicines(medicineId);
            if(index == -1)
            {
                throw new Exception();
            }
            ((Storage)roomController.GetRoom("storage")).MedicineList.RemoveAt(index);
            medicineView.RefreshMedicines();
            this.Close();
        }

        private void DeleteNoDrugs_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public int findMedicine(String medicineId)
        {
            int index = 0;
            var medicineList = ((Storage)roomController.GetRoom("storage")).MedicineList;
            foreach (var medicine in medicineList)
            {
                if (medicine.MedicineID == medicineId)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }
}
