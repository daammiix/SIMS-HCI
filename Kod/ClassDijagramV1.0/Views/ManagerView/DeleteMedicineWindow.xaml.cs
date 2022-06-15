using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.ViewModel;
using System.Windows;

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
