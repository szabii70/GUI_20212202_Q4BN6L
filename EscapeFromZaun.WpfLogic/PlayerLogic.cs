using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static EscapeFromZaun.WpfLogic.GameLogic;

namespace EscapeFromZaun.WpfLogic
{
    public class PlayerLogic
    {
        int width;
        int height;
        public int DrawFromX { get; set; }
        public int DrawFromY { get; set; }
        List<Brush> runRightBrushes;
        List<Brush> runLeftBrushes;
        Brush standRightBrush;
        Brush standLeftBrush;
        Brush jumpRightBrush;
        Brush jumpLeftBrush;
        int frameNumber;
        int actualFrameIndex;
        public PlayerLogic(int width, int height)
        {
            this.width = width;
            this.height = height;

            runLeftBrushes = new List<Brush>();
            runRightBrushes = new List<Brush>();

            //LoadImages();
        }

        private void LoadImages()
        {
            string[] runRight = Directory.GetFiles(@"../../../../EscapeFromZaun//Views/Images/GameImages/JinxMove/RunRight", "*.png");
            string[] runLeft = Directory.GetFiles(@"../../../../EscapeFromZaun//Views/Images/GameImages/JinxMove/RunLeft", "*.png");

            foreach (string run in runRight)
            {
                runLeftBrushes.Add(new ImageBrush(new BitmapImage(new Uri(Path.Combine(run), UriKind.RelativeOrAbsolute))));
            }
            foreach (string run in runLeft)
            {
                runRightBrushes.Add(new ImageBrush(new BitmapImage(new Uri(Path.Combine(run), UriKind.RelativeOrAbsolute))));
            }
            frameNumber = runLeftBrushes.Count();
            actualFrameIndex = 0;

            standRightBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/JinxMove/StandRight.png"), UriKind.RelativeOrAbsolute)));
            standLeftBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/JinxMove/StandLeft.png"), UriKind.RelativeOrAbsolute)));
            jumpRightBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/JinxMove/JumpRight.png"), UriKind.RelativeOrAbsolute)));
            jumpLeftBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/JinxMove/JumpLeft.png"), UriKind.RelativeOrAbsolute)));
        }

        //public Brush PlayerBrush
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}
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
        public Brush PlayerBrush(bool lookRight, bool goingRight, bool goingLeft, bool jumping)
        {
            switch (lookRight)
            {
                case true: //jobbra néz
                    switch (jumping)
                    {
                        case true: //jobbra ugrik
                            //return jumpRightBrush;
                            return Brushes.AliceBlue;
                        case false:
                            switch (goingRight)
                            {
                                case true:
                                    //jobbra mozgó képkockák
                                    return Brushes.Yellow;
                                case false:
                                    //return standRightBrush; //egy helyben áll
                                    return Brushes.Orange;
                            }
                    }

                case false: //balra néz
                    switch (jumping)
                    {
                        case true: //balra ugrik
                            //return jumpLeftBrush;
                            return Brushes.Green;
                        case false:
                            switch (goingLeft)
                            {
                                case true:
                                    //balra mozgó képkockák
                                    return Brushes.Wheat;
                                case false:
                                    //return standLeftBrush; //egy helyben áll
                                    return Brushes.Violet;
                            }
                    }
            }
        }
        public Brush RunBrush()
        {
            actualFrameIndex++;
            if(actualFrameIndex>=frameNumber)
            {
                actualFrameIndex = 0;
            }
            return runRightBrushes[actualFrameIndex];
        }
    }
}
