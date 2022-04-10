using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromZaun.Repository
{
    public class MapGeneratingRepository : IMapGeneratingRepository
    {
        public string[] MapInStringLines()
        {
            string[] lines = File.ReadAllLines("Maps/map1.txt");
            ;
            return lines;
        }
    }
}
