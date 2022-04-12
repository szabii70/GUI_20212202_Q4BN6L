using EscapeFromZaun.WpfLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EscapeFromZaun.Renderer
{
    public class Display : FrameworkElement
    {
        Size windowSize;
        IGameLogic logic;
        PlayerLogic player;

        public Brush BackgroundBrush
        {
            get
            {
                Brush brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Views/Images/GameImages/zaun2.jpg"), UriKind.RelativeOrAbsolute)));
                return brush;
            }
        }
        public void SetupSizes(Size size)
        {
            this.windowSize = size;
        }
        public void SetupLogic(IGameLogic logic)
        {
            this.logic = logic;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if(logic!=null)
            {
                player = logic.MainPlayer;
                drawingContext.DrawRectangle(BackgroundBrush, null,logic.BackgroundRect);
                //drawingContext.DrawGeometry(Brushes.Red, null, player.Hitbox); //Hitbox
                drawingContext.DrawGeometry(logic.PlayerBrush, null, player.CharacterFrame); //Player

                foreach (var item in logic.Platforms)
                {
                    drawingContext.DrawGeometry(item.ItemBrush, null, item.Area);
                }
            }
        }
    }
}
