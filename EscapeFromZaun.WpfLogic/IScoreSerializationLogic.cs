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
        void SetupCollections(IList<PlayerModel> list);
        void DeSerialize(string fileName);

        void FirstPageList(IList<PlayerModel> updatedplayers, int take);

        void NextCommand(IList<PlayerModel> sumPlayers, IList<PlayerModel> justFive,
           ref int skipped, int take, ref int hozzaadott);

        int FirstTake();

        int SecondTake();

        void PreviousCommand(IList<PlayerModel> sumPlayers, IList<PlayerModel> justFive, int take, ref int update);
    }
}
