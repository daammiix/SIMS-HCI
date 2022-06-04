using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.ViewModel;
using System.Windows;

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
