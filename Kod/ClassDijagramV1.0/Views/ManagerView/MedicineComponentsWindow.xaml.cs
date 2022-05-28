using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.ViewModel;
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
    /// Interaction logic for MedicineComponentsWindow.xaml
    /// </summary>
    public partial class MedicineComponentsWindow : Window
    {
        private MedicineComponentsViewModel _medicineComponentsViewModel;
        public MedicineComponentsWindow(QuantifiedMedicine? quantifiedMedicine)
        {
            InitializeComponent();

            _medicineComponentsViewModel = new MedicineComponentsViewModel(this, quantifiedMedicine);
            this.DataContext = _medicineComponentsViewModel;
        }
    }
}
