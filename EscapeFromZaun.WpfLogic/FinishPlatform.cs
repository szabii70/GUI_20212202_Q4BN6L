using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EscapeFromZaun.WpfLogic
{
    public class FinishPlatform : Platform
    {
        public FinishPlatform(int drawFromX, int drawFromY, int width, int height) : base(drawFromX, drawFromY, width, height)
        {
        }
        public override Brush ItemBrush
        {
            get
            {
                return Brushes.Yellow;
            }
        }
    }
}
