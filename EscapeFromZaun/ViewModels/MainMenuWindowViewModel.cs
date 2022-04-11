using EscapeFromZaun.WpfLogic;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace EscapeFromZaun.ViewModels
{
    public class MainMenuWindowViewModel : ObservableRecipient
    {
        IRndBackgroundLogic _backgroundLogic;

        public ICommand PlayCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand QuitCommand { get; set; }
        public string BackgroundImage { get; set; }

        public MainMenuWindowViewModel(IRndBackgroundLogic logic)
        {
            _backgroundLogic = logic;
            BackgroundImage = $"/Views/Images/MainMenuBackground/{logic.GetRandomImage()}";
            ;
            PlayCommand = new RelayCommand(() => this.PlayButtonClick());
            SettingsCommand = new RelayCommand(() => this.SettingsButtonClick());
            QuitCommand = new RelayCommand(() => this.QuitButtonClick());
        }

        public MainMenuWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IRndBackgroundLogic>())
        {

        }

        private void QuitButtonClick()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();
        }

        private void SettingsButtonClick()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new SettingsMenuWindowViewModel();
        }

        private void PlayButtonClick()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new PlayMenuWindowViewModel();
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
