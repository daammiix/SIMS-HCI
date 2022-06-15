using ClassDijagramV1._0.Model;
using ClassDijagramV1._0.Util;
using ClassDijagramV1._0.ViewModel;
using System;
using System.Windows;


namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for ChangedAccountView.xaml
    /// </summary>
    public partial class ChangedAccountView : Window
    {
        private ChangeManagerAccountViewModel changedManagedAccountViewModel;
        public ChangedAccountView(AccountViewModel accountViewModel, ManagerAccount manager)
        {
            InitializeComponent();

            changedManagedAccountViewModel = new ChangeManagerAccountViewModel(this, accountViewModel, manager);
            this.DataContext = changedManagedAccountViewModel;
        }
    }
}
