using EscapeFromZaun.Model;
using EscapeFromZaun.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromZaun.WpfLogic
{
    public class ScoreSerializationLogic : IScoreSerializationLogic
    {
        IScoreSerializationRepository scoreRepositry;
        public ScoreSerializationLogic(IScoreSerializationRepository scoreRepositry)
        {
            this.scoreRepositry = scoreRepositry;
        }

        IList<PlayerModel> players;

        public void SetupCollections(IList<PlayerModel> list)
        {
            scoreRepositry.SetupColllections(list);
        }

        public void DeSerialize(string fileName)
        {
            scoreRepositry.DeSerialize(fileName);
        }

        public void FirstPageList(IList<PlayerModel> updatedplayers, int take)
        {
            scoreRepositry.FirstPageList(updatedplayers, take);
        }

        public void NextCommand(IList<PlayerModel> sumPlayers, IList<PlayerModel> justFive,
           ref int skipped, int take, ref int hozzaadott)
        {
            scoreRepositry.NextCommand(sumPlayers, justFive, ref skipped, take, ref hozzaadott);
        }

        public int FirstTake()
        {
            return scoreRepositry.FirstTake();
        }

        public int SecondTake()
        {
            return scoreRepositry.SecondTake();
        }

        public void PreviousCommand(IList<PlayerModel> sumPlayers, IList<PlayerModel> justFive, int take, ref int update)
        {
            scoreRepositry.PreviousCommand(sumPlayers, justFive, take, ref update);
        }
            

    }
}
