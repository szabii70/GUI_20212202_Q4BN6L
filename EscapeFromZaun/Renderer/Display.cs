using EscapeFromZaun.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EscapeFromZaun.Renderer
{
    public class Display : FrameworkElement
    {
        Size windowSize;
        IGameLogic logic;
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
                drawingContext.DrawEllipse(Brushes.Red, null, new Point(300, 300), 20, 20);
            }
        }
    }
}
