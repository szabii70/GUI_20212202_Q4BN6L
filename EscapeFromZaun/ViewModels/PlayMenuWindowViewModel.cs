using EscapeFromZaun.Logic;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EscapeFromZaun.ViewModels
{
    public class PlayMenuWindowViewModel
    {

        IScoreSerializationLogic logic;
        public PlayMenuWindowViewModel(IScoreSerializationLogic logic)
        {
            this.logic = logic;


            this.NewGameClick = new RelayCommand(() => this.NewGameClick_Button());
            this.LoadGameClick = new RelayCommand(() => this.LoadGameClick_Button());
            this.BackToMainMenuClick = new RelayCommand(() => this.BackToMainMenuClick_Button());
        }

        public ICommand NewGameClick { get; set; }
        public ICommand LoadGameClick { get; set; }
        public ICommand BackToMainMenuClick { get; set; }

        private void NewGameClick_Button()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new GameWindowViewModel();
        }

        private void LoadGameClick_Button()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new GameWindowViewModel();
        }

        private void BackToMainMenuClick_Button()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new MainMenuWindowViewModel();
        }

    }
}
