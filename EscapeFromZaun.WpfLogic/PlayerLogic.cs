using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static EscapeFromZaun.WpfLogic.GameLogic;

namespace EscapeFromZaun.WpfLogic
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
        public Geometry Hitbox
        {
            get
            {
                return new RectangleGeometry(new System.Windows.Rect(DrawFromX, DrawFromY, width, height));
            }
        }
        public Geometry CharacterFrame
        {
            get
            {
                return null;
            }
        }
        public void Move(Directions direction)
        {
            switch (direction)
            {
                case Directions.right:
                    DrawFromX += 10;
                    break;
                case Directions.left:
                    DrawFromX -= 10;
                    break;
                default:
                    break;
            }
        }

        public bool IsCollision(Geometry otherArea)
        {
            return Geometry.Combine(this.Hitbox, otherArea, GeometryCombineMode.Intersect, null).GetArea() > 0;
        }
    }
}
