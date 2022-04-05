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
        void SetupColllections(IList<PlayerModel> list);
        void DeSerialize(string Filename);
    }
}
