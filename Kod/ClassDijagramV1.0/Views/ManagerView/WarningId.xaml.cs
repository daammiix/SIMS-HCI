using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for WarningId.xaml
    /// </summary>
    public partial class WarningId : Window
    {
        public WarningId()
        {
            InitializeComponent();
        }

        private void WarningOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
