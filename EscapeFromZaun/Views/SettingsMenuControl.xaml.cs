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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EscapeFromZaun.Views
{
    /// <summary>
    /// Interaction logic for SettingsMenuControl.xaml
    /// </summary>
    public partial class SettingsMenuControl : UserControl
    {
        public SettingsMenuControl()
        {
            InitializeComponent();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.InvalidateVisual();
        }
    }
}
