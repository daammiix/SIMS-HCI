using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.ViewModel;
using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for ChangeDrugsWindow.xaml
    /// </summary>
    public partial class ChangeMedicineWindow : Window
    {
        private ChangeMedicineViewModel _changeMedicine;
        public ChangeMedicineWindow(QuantifiedMedicine? quantifiedMedicine, IRefreshableMedicineView medicineView)
        {
            InitializeComponent();

            _changeMedicine = new ChangeMedicineViewModel(this, quantifiedMedicine, medicineView);
            this.DataContext = _changeMedicine;
        }
    }
}
