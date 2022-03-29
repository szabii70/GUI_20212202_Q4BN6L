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
    public class SettingsMenuWindowViewModel : ObservableRecipient
    {
        public ICommand BackToMenuCommand { get; set; }
        public SettingsMenuWindowViewModel()
        {
            BackToMenuCommand = new RelayCommand(() => BackToMenuButton_CLick());
        }

        private void BackToMenuButton_CLick()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new MainMenuWindowViewModel();
        }
    }
}
