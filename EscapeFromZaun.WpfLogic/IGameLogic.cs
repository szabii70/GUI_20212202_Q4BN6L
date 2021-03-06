using EscapeFromZaun.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EscapeFromZaun.WpfLogic
{
    public interface IGameLogic
    {
        public event EventHandler GameFinished;
        public event EventHandler GamePaused;

       // public IRndBackgroundLogic backgroundLogic { get; set; }
        PlayerModel Player { get; set; }
        public PlayerLogic MainPlayer { get; set; }
        public List<Platform> Platforms { get; set; }
        public Rect BackgroundRect { get; }
        public Brush PlayerBrush { get; set; }
        public Brush EnemyBrush { get; set; }
        public List<Enemy> Enemies { get; set; }

        public List<Bullet> Bullets { get; set; }
        public void SetupSizes(System.Windows.Size size);
        public void Move(Key key);
        public void NotMove(Key key);
        public void TimeStep();
    }
}