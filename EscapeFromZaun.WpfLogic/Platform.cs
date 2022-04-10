using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EscapeFromZaun.WpfLogic
{
    public class Platform : GameBlockItem
    {
        public int DrawFromX { get; set; }
        public int DrawFromY { get; set; }
        int width;
        int height;


        public Platform(int drawFromX, int drawFromY, int width, int height)
        {
            this.DrawFromX = drawFromX;
            this.DrawFromY = drawFromY;
            this.width = width;
            this.height = height;
        }
        public override Geometry Area
        {
            get
            {
                return new RectangleGeometry(new System.Windows.Rect(DrawFromX, DrawFromY, width, height));
            }
        }

        public override Brush ItemBrush
        {
            get
            {
                //return Brushes.Blue;
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/Platform.png"), UriKind.RelativeOrAbsolute)));
            }
        }
    }
}
