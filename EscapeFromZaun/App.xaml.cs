﻿using EscapeFromZaun.Logic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EscapeFromZaun
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<IRndBackgroundLogic, RndBackgroundLogic>()
                .AddSingleton<IGameLogic, GameLogic>()
                .AddSingleton<IScoreSerializationLogic, ScoreSerializationLogic>()
                .BuildServiceProvider());
        }
    }
}
