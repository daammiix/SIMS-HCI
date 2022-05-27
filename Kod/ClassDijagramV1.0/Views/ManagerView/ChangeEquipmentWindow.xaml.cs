using ClassDijagramV1._0.Controller;
using ClassDijagramV1._0.Helpers;
using ClassDijagramV1._0.Model;
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
using ClassDijagramV1._0.Helpers;
using Controller;
using ClassDijagramV1._0.Model.Enums;
using ClassDijagramV1._0.ViewModel;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for ChangeEquipmentWindow.xaml
    /// </summary>
    public partial class ChangeEquipmentWindow : Window
    {
        private ChangeEquipmentViewModel _changeEquipment;

        public ChangeEquipmentWindow(QuantifiedEquipment? quantifiedEquipment, IRefreshableEquipmentView equipmentView)
        {
            InitializeComponent();

            _changeEquipment = new ChangeEquipmentViewModel(this, quantifiedEquipment, equipmentView);
            this.DataContext = _changeEquipment;
        }
    }
}
