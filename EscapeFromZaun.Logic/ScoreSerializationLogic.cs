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

        public void SetupColllections(IList<PlayerModel> players)
        {
            foreach (var item in players)
            {
                players.Add(item);
            }
        }

        public void DeSerialize(string Filename)
        {
            var input = File.ReadAllText(Filename);
            players = JsonConvert.DeserializeObject<IList<PlayerModel>>(input);
        }

    }
}
