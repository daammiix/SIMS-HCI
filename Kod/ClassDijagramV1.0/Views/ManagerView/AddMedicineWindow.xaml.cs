using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.ViewModel;
using System.Windows;

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
