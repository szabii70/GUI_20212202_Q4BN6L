using EscapeFromZaun.Model;
using EscapeFromZaun.Repository;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EscapeFromZaun.WpfLogic
{
    public class ScoreSerializationLogic : IScoreSerializationLogic
    {
        IScoreSerializationRepository scoreRepositry;
        IGameLogic gameLogic;
        public PlayerModel player { get; set; }

        public ScoreSerializationLogic(IScoreSerializationRepository scoreRepositry)
        {
            this.scoreRepositry = scoreRepositry;
            this.player = new PlayerModel();
            gameLogic = Ioc.Default.GetService<IGameLogic>();
            this.player.Outcome = this.gameLogic.Player.Outcome;
            this.player.PlayerRunTime = this.gameLogic.Player.PlayerRunTime;

        }

        public ScoreSerializationLogic() : this(IsInDesignMode ? null : Ioc.Default.GetService<IScoreSerializationRepository>())
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

        public void WriteToJson(string playername)
        {
            this.player.Name = playername;
            scoreRepositry.WriteToJson(player);
        }


    }
}
