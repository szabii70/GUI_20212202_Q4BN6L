using EscapeFromZaun.Views;
using EscapeFromZaun.WpfLogic;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
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
        IGameLogic logic;
        public PlayMenuWindowViewModel():this(IsInDesignMode ? null : Ioc.Default.GetService<IGameLogic>())
        {

        }
        public PlayMenuWindowViewModel(IGameLogic logic)
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
            //Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new GameWindowViewModel();
            new GameWindow(logic).ShowDialog();
        }

        private void LoadGameClick_Button()
        {
            //Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new GameWindowViewModel();
            new GameWindow(logic).ShowDialog();
        }

        private void BackToMainMenuClick_Button()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new MainMenuWindowViewModel();
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
    }
}
