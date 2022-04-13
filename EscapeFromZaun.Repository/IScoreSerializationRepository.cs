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

        void FirstPageList(IList<PlayerModel> updatedplayers, int take);

        void NextCommand(IList<PlayerModel> sumPlayers, IList<PlayerModel> justFive,
           ref int skipped, int take, ref int hozzaadott);

        int SecondTake();

        int FirstTake();


        void PreviousCommand(IList<PlayerModel> sumPlayers, IList<PlayerModel> justFive, int take,ref int skip, ref int update);
        public void WriteToJson(PlayerModel player);
        public void ReadFromJson();
    }
}
