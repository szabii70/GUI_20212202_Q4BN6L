using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromZaun.WpfLogic
{
    public class BackgroundDatas
    {
        double width;
        double height;
        public int DrawFromX { get; set; }
        public int DrawFromY { get; set; }

        public BackgroundDatas(double width, double height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
