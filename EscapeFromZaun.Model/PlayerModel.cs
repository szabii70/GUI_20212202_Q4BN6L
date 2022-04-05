using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace EscapeFromZaun.Model
{
    public class PlayerModel : ObservableObject
    {
        public string Name { get; set; }
        public string Outcome { get; set; }
        public DateTime PlayerRunTime { get; set; }
    }
}
