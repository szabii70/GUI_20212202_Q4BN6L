using EscapeFromZaun.Model;
using EscapeFromZaun.Views;
using EscapeFromZaun.WpfLogic;

using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EscapeFromZaun.ViewModels
{
    public class PlayMenuWindowViewModel
    {
        IGameLogic gameLogic;
        IScoreSerializationLogic logic;
        int skipped;
        int addded;
        int updated;
        public ObservableCollection<PlayerModel> Players { get; set; }
        public ObservableCollection<PlayerModel> JustFive { get; set; }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public PlayMenuWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IScoreSerializationLogic>(), Ioc.Default.GetService<IGameLogic>())
        {

        }


        public ICommand NextCommand { get; set; }

        public ICommand PreviousCommand { get; set; }

        public ICommand NewGameClick { get; set; }
        public ICommand LoadGameClick { get; set; }
        public ICommand BackToMainMenuClick { get; set; }

        public PlayMenuWindowViewModel(IScoreSerializationLogic logic, IGameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
            skipped = 5;
            addded = 0;
            updated = 0;
            this.logic = logic;
            Players = new ObservableCollection<PlayerModel>();
            JustFive = new ObservableCollection<PlayerModel>();

            logic.DeSerialize("Views/Game.json");

            logic.SetupCollections(Players);

            int takefirst = logic.FirstTake();

            logic.FirstPageList(JustFive, takefirst);

            int takesecond = logic.SecondTake();


            NextCommand = new RelayCommand(
                () => logic.NextCommand(Players, JustFive, ref skipped, takesecond, ref addded),
                () => (addded + takefirst) != (takefirst + takesecond));

            PreviousCommand = new RelayCommand(() => logic.PreviousCommand(Players, JustFive, takefirst, ref updated));


            this.NewGameClick = new RelayCommand(() => this.NewGameClick_Button());
            this.LoadGameClick = new RelayCommand(() => this.LoadGameClick_Button());
            this.BackToMainMenuClick = new RelayCommand(() => this.BackToMainMenuClick_Button());
        }
        

        private void NewGameClick_Button()
        {
            //Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new GameWindowViewModel();
            var window = new GameWindow(gameLogic);
            window.GameFinished += Window_GameFinished;
            window.ShowDialog();
        }

        private void LoadGameClick_Button()
        {
            //Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new GameWindowViewModel();
            var window = new GameWindow(gameLogic);
            window.GameFinished += Window_GameFinished;
            window.ShowDialog();
        }

        private void BackToMainMenuClick_Button()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new MainMenuWindowViewModel();
        }

        private void Window_GameFinished(object? sender, EventArgs e)
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new ScoreMenuWindowViewModel();
        }
    }
}
