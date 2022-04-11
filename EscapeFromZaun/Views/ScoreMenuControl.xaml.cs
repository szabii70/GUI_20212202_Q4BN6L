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
    /// Interaction logic for ScoreMenuControl.xaml
    /// </summary>
    public partial class ScoreMenuControl : UserControl
    {
        public ScoreMenuControl()
        {
            InitializeComponent();
        }

        private void tb_write_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_write.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
