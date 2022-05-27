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
    /// Interaction logic for ChangeRejectedDrugsWindow.xaml
    /// </summary>
    public partial class ChangeRejectedMedicineWindow : Window
    {
        private ChangeRejectedMedicineViewModel _changeRejectedMedicine;
        public ChangeRejectedMedicineWindow(QuantifiedMedicine? quantifiedMedicine, IRefreshableMedicineView medicineView)
        {
            InitializeComponent();

            _changeRejectedMedicine = new ChangeRejectedMedicineViewModel(this, quantifiedMedicine, medicineView);
            this.DataContext = _changeRejectedMedicine;
        }
    }
}
