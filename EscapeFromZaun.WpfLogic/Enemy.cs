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
    public class Enemy : GameBlockItem
    {
        public int DrawFromX { get; set; }
        public int DrawFromY { get; set; }
        int width;
        int height;
        
        

        public Enemy(int drawFromX, int drawFromY, int width, int height, int health, int damage)
        {
            this.DrawFromX = drawFromX;
            this.DrawFromY = drawFromY;
            this.width = width;
            this.height = height;
            this.health = health;
            this.Damage = damage;
            
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

        public Brush EnemyBrush(bool left, bool right)
        {
            if (left == true)
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/caitlynleft.png"), UriKind.RelativeOrAbsolute)));
            }
            else if(right == true)
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/caitlyn2.png"), UriKind.RelativeOrAbsolute)));
            }
            return new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/caitlynleft.png"), UriKind.RelativeOrAbsolute)));
        }

        public override Brush ItemBrush
        {
            get
            {
                //return Brushes.Red;
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/caitlynleft.png"), UriKind.RelativeOrAbsolute)));
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
