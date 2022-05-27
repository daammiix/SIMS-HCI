﻿using Controller;
using ClassDijagramV1._0.Views.ManagerView;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassDijagramV1._0.ViewModel;

namespace ClassDijagramV1._0.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for StorageView.xaml
    /// </summary>
    public partial class StorageView : UserControl
    {
        private StorageViewModel _storageViewModel;
        public StorageView()
        {
            InitializeComponent();

            _storageViewModel = new StorageViewModel();
            this.DataContext = _storageViewModel;
        }
    }
}
