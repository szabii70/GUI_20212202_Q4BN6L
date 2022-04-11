using EscapeFromZaun.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json.Serialization;

namespace EscapeFromZaun.Repository
{
    public class ScoreSerializationRepository : IScoreSerializationRepository
    {
       ObservableCollection<PlayerModel> players { get; set; }
       public ScoreSerializationRepository()
        {
            players = new ObservableCollection<PlayerModel>();
        }
        public void WriteToJson(PlayerModel player)
        {
            ReadFromJson();
            PlayerModel player1 = new PlayerModel();
            player1 = player;
            players.Add(player1);
            string s = JsonConvert.SerializeObject(players, Formatting.Indented);
            File.WriteAllText("highscores.json", s);

        }

        public void ReadFromJson()
        {
            if (File.Exists("highscores.json"))
            {
                string s = File.ReadAllText("highscores.json");
                players = JsonConvert.DeserializeObject<ObservableCollection<PlayerModel>>(s);
            }
        }
    }
}
