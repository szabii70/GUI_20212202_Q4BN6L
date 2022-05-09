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

                foreach (var item in logic.Enemies)
                {
                    drawingContext.DrawGeometry(item.ItemBrush, null, item.Area);
                    //drawingContext.DrawGeometry(Brushes.Yellow, null, item.CollisonArea);
                }

                foreach (var item in logic.Bullets)
                {
                    drawingContext.DrawEllipse(item.ItemBrush, null, new Point(item.Center.X, item.Center.Y), 10, 10);
                }


                drawingContext.DrawRectangle((SolidColorBrush)new BrushConverter().ConvertFrom("#e5c179"), null, new Rect(0, 0, 180, 100));
                drawingContext.DrawRectangle((SolidColorBrush)new BrushConverter().ConvertFrom("#1E2328"), null, new Rect(5, 5, 170, 90));


                drawingContext.DrawText(
                new FormattedText(this.logic.Player.timestring.ToString(), System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight, new Typeface(new FontFamily("Bookman Old Style"), FontStyles.Normal, FontWeights.Bold, FontStretches.Normal), 40, (SolidColorBrush)new BrushConverter().ConvertFrom("#e5c179")),
                new Point(25, 25));

                ;
                //ÉLET
                drawingContext.DrawRectangle((SolidColorBrush)new BrushConverter().ConvertFrom("#e5c179"), null, new Rect(windowSize.Width - 175 , 0, 180, 100));
                drawingContext.DrawRectangle((SolidColorBrush)new BrushConverter().ConvertFrom("#1E2328"), null, new Rect(windowSize.Width - 170, 5, 165, 90));
                drawingContext.DrawText(
                new FormattedText(this.logic.MainPlayer.Health.ToString(), System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight, new Typeface(new FontFamily("Bookman Old Style"), FontStyles.Normal, FontWeights.Bold, FontStretches.Normal), 40, (SolidColorBrush)new BrushConverter().ConvertFrom("#e5c179")),
                new Point(windowSize.Width - 145, 25));

                drawingContext.DrawEllipse(Brushes.Red, null, new Point(windowSize.Width - 55, 45), 25, 25);
            }
        }

    }
}
