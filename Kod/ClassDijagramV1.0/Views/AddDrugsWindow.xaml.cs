﻿using System;
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

namespace ClassDijagramV1._0.Views
{
    /// <summary>
    /// Interaction logic for AddDrugsWindow.xaml
    /// </summary>
    public partial class AddDrugsWindow : Window
    {
        public AddDrugsWindow()
        {
            InitializeComponent();
        }

        private void SaveAddedDrugs_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void QuitAddedDrugs_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
