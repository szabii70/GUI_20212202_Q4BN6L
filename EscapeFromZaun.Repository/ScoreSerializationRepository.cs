using EscapeFromZaun.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EscapeFromZaun.Repository
{
    public class ScoreSerializationRepository : IScoreSerializationRepository
    {
        IList<PlayerModel> players;

        public void SetupColllections(IList<PlayerModel> list)
        {
            foreach (var item in players)
            {
                list.Add(item);
            }
        }

        public void DeSerialize(string Filename)
        {
            var input = File.ReadAllText(Filename);
            players = JsonConvert.DeserializeObject<IList<PlayerModel>>(input);
            ;
        }
    }
}
