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
using System.Windows.Media;
using System.Windows.Documents;
using EscapeFromZaun.Views;


namespace EscapeFromZaun.ViewModels
{
    public class ScoreMenuWindowViewModel : ObservableRecipient
    {

        public IScoreSerializationLogic serializationlogic { get; set; }

        public IGameLogic gamelogic { get; set; }
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
        public string PlayerRunTime
        {
            get
            {
                return gamelogic.Player.timestring;
            }
        }
        public string PlayerOutcome
        {
            get
            {
                return gamelogic.Player.Outcome;
            }
        }


        private int zindex;

        public int ZIndex
        {
            get { return zindex; }
            set
            {
                SetProperty(ref zindex, value);
            }
        }

        public ScoreMenuWindowViewModel(IScoreSerializationLogic serializationlogic, IGameLogic gamelogic)
        {
            ZIndex = -1;
            this.serializationlogic = serializationlogic;
            this.gamelogic = gamelogic;
            BackgroundImage = $"/Views/Images/Scoremenu.png";
            BackToMenuCommand = new RelayCommand(() => BackToMenuButton_CLick());

            WriteCommand = new RelayCommand(
                () =>
                {
                    if (playerName != "" && playerName != null)
                    {
                        ZIndex = 0;
                        gamelogic.Player.Name = PlayerName;
                        serializationlogic.SavePlayer(gamelogic.Player);
                        SaveConfirmWindowNotEmpty confirm = new SaveConfirmWindowNotEmpty();
                        if ((bool)confirm.ShowDialog())
                        {
                            BackToMenuButton_CLick();
                        }
                        ZIndex = -1;
                    }
                    else
                    {
                        ZIndex = 0;
                        SaveConfirmWindow a = new SaveConfirmWindow();
                        if ((bool)a.ShowDialog())
                        {
                            BackToMenuButton_CLick();
                        }
                        ZIndex = -1;
                    }
                });
        }

        public ScoreMenuWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IScoreSerializationLogic>(), Ioc.Default.GetService<IGameLogic>())
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
