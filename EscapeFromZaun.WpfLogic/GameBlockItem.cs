using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EscapeFromZaun.WpfLogic
{
    public abstract class GameBlockItem
    {
        public abstract Geometry Area { get; }
        public abstract Brush ItemBrush { get; }
        public bool IsCollision(Geometry otherArea)
        {
            return Geometry.Combine(this.Area, otherArea, GeometryCombineMode.Intersect, null).GetArea() > 0;
        }
    }
}
