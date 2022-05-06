using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EscapeFromZaun.WpfLogic
{
    public class Enemy : GameBlockItem
    {
        public int DrawFromX { get; set; }
        public int DrawFromY { get; set; }
        int width;
        int height;


        public Enemy(int drawFromX, int drawFromY, int width, int height, int health)
        {
            this.DrawFromX = drawFromX;
            this.DrawFromY = drawFromY;
            this.width = width;
            this.height = height;
            this.health = health;
            
        }

        private int health;

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public BulletType Type { get; set; }

        public int Damage { get; set; }

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
                return Brushes.Red;
            }
        }

        public Geometry CollisonArea
        {
            get
            {
                return new RectangleGeometry(new System.Windows.Rect(DrawFromX - 100, DrawFromY, width + 200, height - 20));
            }
        }
    }
}
