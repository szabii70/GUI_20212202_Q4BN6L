using EscapeFromZaun.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromZaun.Logic
{
    public interface IScoreSerializationLogic
    {
        void SetupColllections(IList<PlayerModel> players);
        void DeSerialize(string Filename);
    }
}
