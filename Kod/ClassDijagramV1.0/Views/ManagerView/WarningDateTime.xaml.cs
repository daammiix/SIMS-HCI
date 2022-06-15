using System.Windows;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for WarningDateTime.xaml
    /// </summary>
    public partial class WarningDateTime : Window
    {
        public WarningDateTime()
        {
            InitializeComponent();
        }

        private void WarningOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
