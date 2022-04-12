using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Media;

namespace EscapeFromZaun.Logic
{
    public class PlayerLogic
    {
        int width;
        int height;
        public int DrawFromX { get; set; }
        public int DrawFromY { get; set; }
        public PlayerLogic(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public Brush PlayerBrush
        {
            get
            {
                return null;
            }
        }
        public Geometry HitBox
        {
            get
            {
                return null;
            }
        }
    }
}
