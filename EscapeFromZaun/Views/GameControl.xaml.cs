using EscapeFromZaun.WpfLogic;
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
using System.Windows.Threading;

namespace EscapeFromZaun.Views
{
    /// <summary>
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl
    {
        IGameLogic logic;
        DispatcherTimer mainTimer;
        public GameControl()
        {
            InitializeComponent();
            logic = new GameLogic();
            mainTimer = new DispatcherTimer();
            mainTimer.Interval = TimeSpan.FromMilliseconds(5);
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Start();
        }

        private void MainTimer_Tick(object? sender, EventArgs e)
        {
            //logic.TimeStep();
            display.InvalidateVisual();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            display.SetupLogic(logic);
            //logic.SetupSizes(new Size(grid.ActualWidth,grid.ActualHeight));
            display.SetupSizes(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            //logika mozgás
            display.InvalidateVisual();
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            //logika nem mozgás
            display.InvalidateVisual();
        }
    }
}
