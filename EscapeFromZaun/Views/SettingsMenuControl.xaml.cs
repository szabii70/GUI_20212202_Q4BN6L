using EscapeFromZaun.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EscapeFromZaun.Views
{
    /// <summary>
    /// Interaction logic for SettingsMenuControl.xaml
    /// </summary>
    public partial class SettingsMenuControl : UserControl
    {
        public SettingsMenuWindowViewModel vm;
        public SettingsMenuControl()
        {
            InitializeComponent();
             vm = (DataContext as SettingsMenuWindowViewModel);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (vm != null)
            {
            vm._backgroundLogic.sp.Play();

            }
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (vm != null)
            {
                vm._backgroundLogic.sp.Play();

            }
        }

        private void Slider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (vm != null)
            {
                vm._backgroundLogic.sp.Volume = vm._backgroundLogic.EffectSound;
                vm._backgroundLogic.sp.Stop();
                vm._backgroundLogic.sp.Play();
            }
        }
    }
}
