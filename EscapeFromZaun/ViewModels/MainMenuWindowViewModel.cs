using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EscapeFromZaun.ViewModels
{
    public class MainMenuWindowViewModel : ObservableRecipient
    {
        public ICommand PlayCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand QuitCommand { get; set; }
        public string BackgroundImage { get; set; }

        public MainMenuWindowViewModel()
        {
            PlayCommand = new RelayCommand(() => this.PlayButtonClick());
            SettingsCommand = new RelayCommand(() => this.SettingsButtonClick());
            QuitCommand = new RelayCommand(() => this.QuitButtonClick());

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
    }
}
