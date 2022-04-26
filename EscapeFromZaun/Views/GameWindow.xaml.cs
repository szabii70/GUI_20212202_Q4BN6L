using EscapeFromZaun.ViewModels;
using EscapeFromZaun.WpfLogic;
using GalaSoft.MvvmLight;
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
        private DispatcherTimer dt;

        public event EventHandler GameFinished;

        public GameWindow(IGameLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            if (mainTimer is null)
            {
                mainTimer = new DispatcherTimer();
                mainTimer.Interval = TimeSpan.FromMilliseconds(5);
                mainTimer.Tick += MainTimer_Tick;
                mainTimer.Start();
            }

            if (dt is null)
            {
                dt = new DispatcherTimer();
                this.dt.Interval = TimeSpan.FromSeconds(1);
                this.dt.Tick += this.Dt_Tick;
                this.dt.Start();
            }


            logic.GamePaused += Logic_EscapePressed;
            logic.GameFinished += Logic_GameFinished;
        }

        private void Logic_EscapePressed(object? sender, EventArgs e)
        {
            mainTimer.Stop();
            dt.Stop();
            PauseWindow pw = new PauseWindow();
            pw.ShowDialog();
            if (pw.DialogResult == true)
            {
                mainTimer.Start();
                dt.Start();            
            }
            else
            {
                mainTimer.Start();
                dt.Start();
                this.Close();
                Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new MainMenuWindowViewModel();
            }
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            this.logic.Player.PlayerRunTime = logic.Player.PlayerRunTime.AddSeconds(1);
            this.display.InvalidateVisual();
        }

        private void Logic_GameFinished(object? sender, EventArgs e)
        {
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
            this.logic.Player.PlayerRunTime = new DateTime(0);
            
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
            dt.Stop();
        }
    }
}
