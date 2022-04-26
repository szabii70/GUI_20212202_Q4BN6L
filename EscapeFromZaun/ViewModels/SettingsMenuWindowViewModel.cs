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
using System.Windows.Input;

namespace EscapeFromZaun.ViewModels
{
    public class SettingsMenuWindowViewModel : ObservableRecipient
    {
        public IRndBackgroundLogic _backgroundLogic { get; set; }
        public ICommand BackToMenuCommand { get; set; }
        public ICommand MuteCommand { get; set; }

        public bool Muted
        { 
            get
            {
                return _backgroundLogic.Muted;
            }
        }

        public bool MutedSound
        {
            get
            {
                return _backgroundLogic.MutedSound;
            }
        }
        public bool NotMuted
        {
            get
            {
                return !Muted;
            }
        }


        public SettingsMenuWindowViewModel(IRndBackgroundLogic _backgroundLogic)
        {
            this._backgroundLogic = _backgroundLogic;
            BackToMenuCommand = new RelayCommand(() => BackToMenuButton_CLick());
            MuteCommand = new RelayCommand(() => this._backgroundLogic.ToggleMuted());
            Messenger.Register<SettingsMenuWindowViewModel, string, string>(this, "SoundInfo", (recipient, msg) =>
            {
                OnPropertyChanged("Muted");
                OnPropertyChanged("NotMuted");

            });
        }


        public SettingsMenuWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IRndBackgroundLogic>())
        {

        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        private void BackToMenuButton_CLick()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new MainMenuWindowViewModel();
        }
    }
}
