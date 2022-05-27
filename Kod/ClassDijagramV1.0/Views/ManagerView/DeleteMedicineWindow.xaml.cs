using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.ViewModel;
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

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for DeleteDrugs.xaml
    /// </summary>
    public partial class DeleteMedicineWindow : Window
    {
        private DeleteMedicineViewModel _deleteMedicine;
        public DeleteMedicineWindow(QuantifiedMedicine? quantifiedMedicine, IRefreshableMedicineView medicineView)
        {
            InitializeComponent();

            _deleteMedicine = new DeleteMedicineViewModel(this, quantifiedMedicine, medicineView);
            this.DataContext = _deleteMedicine;
        }
    }
}
