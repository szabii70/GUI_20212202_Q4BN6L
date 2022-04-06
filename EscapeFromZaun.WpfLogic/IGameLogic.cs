using EscapeFromZaun.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace EscapeFromZaun.WpfLogic
{
    public interface IGameLogic
    {
        PlayerModel Player { get; set; }
        public PlayerLogic MainPlayer { get; set; }
        public List<Platform> Platforms { get; set; }
        public void SetupSizes(Size size);
        public void Move(Key key);
        public void NotMove(Key key);
        public void TimeStep();
    }
}