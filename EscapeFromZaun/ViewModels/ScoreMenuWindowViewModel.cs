using EscapeFromZaun.Model;
using EscapeFromZaun.WpfLogic;
using Microsoft.Toolkit.Mvvm.ComponentModel;
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
    public class ScoreMenuWindowViewModel : ObservableRecipient
    {
        public IScoreSerializationLogic serializationlogic { get; set; }
        public PlayerModel currentPlayer { get; set; }
        public ICommand BackToMenuCommand { get; set; }

        public ScoreMenuWindowViewModel(IScoreSerializationLogic serializationlogic)
        {
            this.serializationlogic = serializationlogic;
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
    }
}
