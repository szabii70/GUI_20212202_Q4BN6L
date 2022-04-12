using EscapeFromZaun.Model;
using EscapeFromZaun.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromZaun.Logic
{
    public class ScoreSerializationLogic : IScoreSerializationLogic
    {
        IScoreSerializationRepository scoreRepositry;
        public ScoreSerializationLogic(IScoreSerializationRepository scoreRepositry)
        {
            this.scoreRepositry = scoreRepositry;
        }


        IList<PlayerModel> players;

        public void SetupColllections(IList<PlayerModel> list)
        {
            scoreRepositry.SetupColllections(list);
        }

        public void DeSerialize(string Filename)
        {
            scoreRepositry.DeSerialize(Filename);
        }

    }
}
