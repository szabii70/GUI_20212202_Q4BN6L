using System;
using System.Text.Json.Serialization;

namespace EscapeFromZaun.Repository
{
    public class ScoreSerializationRepository : IScoreSerializationRepository
    {
        public JsonConverter json { get; set; } 
        public ScoreSerializationRepository()
        {
            
        }
    }
}
