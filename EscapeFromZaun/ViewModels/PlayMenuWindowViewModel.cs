using EscapeFromZaun.Logic;
using EscapeFromZaun.Model;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
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

        IScoreSerializationLogic logic;

        public ObservableCollection<PlayerModel> Players { get; set; }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public PlayMenuWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IScoreSerializationLogic>())
        {

        }

        public PlayMenuWindowViewModel(IScoreSerializationLogic logic)
        {
            this.logic = logic;
            Players = new ObservableCollection<PlayerModel>();

            logic.DeSerialize("C:/Users/User/Desktop/Game.json");

            logic.SetupColllections(Players);

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
