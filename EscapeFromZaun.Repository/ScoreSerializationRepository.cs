using EscapeFromZaun.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public void FirstPageList(IList<PlayerModel> updatedplayers, int take)
        {
            var x = players.OrderBy(t => t.PlayerRunTime).Take(take).ToList();

            foreach (var item in x)
            {
                updatedplayers.Add(item);
            }
            ;
        }

        public void NextCommand(IList<PlayerModel> sumPlayers, IList<PlayerModel> justFive,
           ref int skipped, int take, ref int hozzaadott)
        {

            var z = sumPlayers.OrderBy(t => t.PlayerRunTime).Skip(skipped).Take(take).ToList();
            var addnew = sumPlayers.Count() - justFive.Count();
            var fordelete = justFive.Count() - addnew;
            ;
            if (z.Count != 0)
            {
                for (int i = 0; i < addnew; i++)
                {
                    if (justFive.Contains(justFive[i]))
                    {
                        justFive[i] = z[i];
                        hozzaadott++;
                        ;
                    }
                }
                int ciklusindulo = justFive.Count() - 1;
                int cikluszaro = justFive.Count() - fordelete;
                if (z != null)
                {
                    for (int i = ciklusindulo; i >= cikluszaro; i--)
                    {
                        justFive.Remove(justFive[i]);
                    }
                }
            }
            skipped = skipped + 5;
        }

        public int SecondTake()
        {
            if (players.Count > 5 && players.Count <= 10)
            {
                return players.Count - 5;
            }
            return 5;
        }

        public int FirstTake()
        {
            if (players.Count <= 5)
            {
                return players.Count;
            }
            else
            {
                return 5;
            }
        }

        public void PreviousCommand(IList<PlayerModel> sumPlayers, IList<PlayerModel> justFive, int take, ref int update)
        {
            var z = sumPlayers.OrderBy(t => t.PlayerRunTime).Take(take).ToList();

            var count = sumPlayers.Take(take).Count();
            var div = justFive.Count();
            var end = z.Count();

            for (int i = 0; i < justFive.Count(); i++)
            {
                justFive[i] = z[i];
                update++;
            }
            if (z != null)
            {
                for (int i = div; i < end; i++)
                {
                    justFive.Add(z[i]);
                }
            }
        }
    }
}
