using EscapeFromZaun.Model;
using EscapeFromZaun.WpfLogic;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
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
    public class ScoreMenuWindowViewModel : ObservableRecipient
    {

        public IScoreSerializationLogic serializationlogic { get; set; }
        public ICommand BackToMenuCommand { get; set; }
        public ICommand WriteCommand { get; set; }
        public string BackgroundImage { get; set; }

        private string playerName;
        public string PlayerName
        {
            get
            {
                return playerName;
            }
            set
            {
                SetProperty(ref playerName, value);
                (WriteCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ScoreMenuWindowViewModel(IScoreSerializationLogic serializationlogic)
        {
            this.serializationlogic = serializationlogic;
            BackgroundImage = $"/Views/Images/Scoremenu.png";
            BackToMenuCommand = new RelayCommand(() => BackToMenuButton_CLick());
            WriteCommand = new RelayCommand(
                () =>
                {
                    if (playerName != "" && playerName != null)
                    {
                        serializationlogic.WriteToJson(playerName);
                        BackToMenuButton_CLick();
                    }
                    else
                    {
                        BackToMenuButton_CLick();
                    }
                });
        }

        public ScoreMenuWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IScoreSerializationLogic>())
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
