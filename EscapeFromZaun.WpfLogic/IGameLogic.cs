﻿using EscapeFromZaun.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace EscapeFromZaun.WpfLogic
{
    public interface IGameLogic
    {
        PlayerModel Player { get; set; }
        public PlayerLogic MainPlayer { get; set; }
        public List<Platform> Platforms { get; set; }
        public Rect BackgroundRect { get; }
        public void SetupSizes(System.Windows.Size size);
        public void Move(Key key);
        public void NotMove(Key key);
        public void TimeStep();
    }
}