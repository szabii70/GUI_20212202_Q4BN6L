using EscapeFromZaun.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromZaun.WpfLogic
{
    public interface IScoreSerializationLogic
    {
       public void WriteToJson(string playername);
        public PlayerModel player { get; set; }


    }
}
