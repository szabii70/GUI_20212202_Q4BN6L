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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EscapeFromZaun.Views
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        IGameLogic logic;
        DispatcherTimer mainTimer;
        public event EventHandler GameFinished;
        public GameWindow(IGameLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            mainTimer = new DispatcherTimer();
            mainTimer.Interval = TimeSpan.FromMilliseconds(5);
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Start();

            logic.GameFinished += Logic_GameFinished;
        }

        private void Logic_GameFinished(object? sender, EventArgs e)
        {
            ;
            GameFinished(this,null);
            this.Close();
        }

        private void MainTimer_Tick(object? sender, EventArgs e)
        {
            logic.TimeStep();
            display.InvalidateVisual();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.SetupLogic(logic);
            logic.SetupSizes(new Size(grid.ActualWidth, grid.ActualHeight));
            display.SetupSizes(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            logic.Move(e.Key);
            display.InvalidateVisual();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            logic.NotMove(e.Key);
            display.InvalidateVisual();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainTimer.Stop();
        }
    }
}
