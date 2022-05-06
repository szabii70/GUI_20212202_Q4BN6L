using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace EscapeFromZaun.WpfLogic
{
    public enum BulletType { player, enemy }

    public class Bullet
    {
        public Bullet(System.Drawing.Point from, Vector to)
        {
            Center = from;
            Speed = to;
        }

        public System.Drawing.Point Center { get; set; }

        public Vector Speed { get; set; }

        public Brush ItemBrush
        {
            get
            {
                return Brushes.Yellow;
            }

        }

        public BulletType Type { get; set; }

        public bool Move(System.Windows.Size area)
        {
            //hova kerülne a lépéskor a lövedék
            System.Drawing.Point newCenter =
                new System.Drawing.Point(Center.X + (int)Speed.X,
                Center.Y + (int)Speed.Y);
            if (newCenter.X >= 0 &&
                newCenter.X <= area.Width &&
                newCenter.Y >= 0 &&
                newCenter.Y <= area.Height
                )
            {
                //pályán belül van a lövedék
                

                Center = newCenter;
                
                return true;
            }
            else
            {
                //épp elhagyta a pályát
                return false;
            }
        }
    }
}
