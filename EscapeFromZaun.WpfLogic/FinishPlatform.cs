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
    public class FinishPlatform : Platform
    {
        public FinishPlatform(int drawFromX, int drawFromY, int width, int height) : base(drawFromX, drawFromY, width, height)
        {
        }
        public override Brush ItemBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/Finish.png"), UriKind.RelativeOrAbsolute)));
            }
        }
    }
}
