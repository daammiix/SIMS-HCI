using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.ViewModel;
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
    public partial class AddMedicineWindow : Window
    {
        private AddMedicineViewModel _addMedicine;
        public AddMedicineWindow(IRefreshableMedicineView medicineView)
        {
            InitializeComponent();

            _addMedicine = new AddMedicineViewModel(this, medicineView);
            this.DataContext = _addMedicine;
        }
    }
}
