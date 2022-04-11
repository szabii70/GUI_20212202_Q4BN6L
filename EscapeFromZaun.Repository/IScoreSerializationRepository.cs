using EscapeFromZaun.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromZaun.Repository
{
    public interface IScoreSerializationRepository
    {
        public void WriteToJson(PlayerModel player);
        public void ReadFromJson();
    }
}
