using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.ViewModel;
using System.Windows;

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
