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
        bool lookRight;
        public int DrawFromX { get; set; }
        public int DrawFromY { get; set; }
        List<Brush> runRightBrushes;
        List<Brush> runLeftBrushes;
        List<Brush> jumpRightBrushes;
        List<Brush> jumpLeftBrushes;
        Brush standRightBrush;
        Brush standLeftBrush;
        int frameRunNumber;
        int frameJumpNumber;
        int actualRunFrameIndex;
        int actualJumpFrameIndex;

        private int health;

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Damage { get; set; }
        public PlayerLogic(int width, int height, int health, int damage)
        {
            this.width = width;
            this.height = height;
            this.health = health;
            this.Damage = damage;
            
            LoadImages();
        }

        

        private void LoadImages()
        {
            runLeftBrushes = new List<Brush>();
            runRightBrushes = new List<Brush>();
            jumpLeftBrushes = new List<Brush>();
            jumpRightBrushes = new List<Brush>();

            string[] runRight = Directory.GetFiles(@"../../../../EscapeFromZaun//Views/Images/GameImages/JinxMove/RunRight2", "*.png");
            string[] runLeft = Directory.GetFiles(@"../../../../EscapeFromZaun//Views/Images/GameImages/JinxMove/RunLeft", "*.png");

            string[] jumpRight = Directory.GetFiles(@"../../../../EscapeFromZaun//Views/Images/GameImages/JinxMove/JumpRight", "*.png");
            string[] jumpLeft = Directory.GetFiles(@"../../../../EscapeFromZaun//Views/Images/GameImages/JinxMove/JumpLeft", "*.png");

            foreach (string run in runRight)
            {
                runRightBrushes.Add(new ImageBrush(new BitmapImage(new Uri(Path.Combine(run), UriKind.RelativeOrAbsolute))));
            }
            foreach (string run in runLeft)
            {
                runLeftBrushes.Add(new ImageBrush(new BitmapImage(new Uri(Path.Combine(run), UriKind.RelativeOrAbsolute))));
            }

            foreach (string item in jumpLeft)
            {
                jumpLeftBrushes.Add(new ImageBrush(new BitmapImage(new Uri(Path.Combine(item), UriKind.RelativeOrAbsolute))));
            }

            foreach (string item in jumpRight)
            {
                jumpRightBrushes.Add(new ImageBrush(new BitmapImage(new Uri(Path.Combine(item), UriKind.RelativeOrAbsolute))));
            }

            frameRunNumber = runLeftBrushes.Count();
            frameJumpNumber = jumpLeftBrushes.Count();
            actualRunFrameIndex = 0;
            actualJumpFrameIndex = 0;

            standRightBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/JinxMove/StandRight.png"), UriKind.RelativeOrAbsolute)));
            standLeftBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine(@"../../../../EscapeFromZaun/Views/Images/GameImages/JinxMove/StandLeft.png"), UriKind.RelativeOrAbsolute)));
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
                return new RectangleGeometry(new System.Windows.Rect(DrawFromX+130, DrawFromY+30, width-250, height-30));
            }
        }
        public Geometry CharacterFrame
        {
            get
            {
                return new RectangleGeometry(new System.Windows.Rect(DrawFromX, DrawFromY, width, height));
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
        public Brush PlayerBrush(bool lookRight, bool goingRight, bool goingLeft, bool onFloor)
        {
            this.lookRight = lookRight;
            switch (lookRight)
            {
                case true: //jobbra néz
                    switch (onFloor)
                    {
                        case false: //jobbra ugrik
                            return JumpBrush();
                            //return Brushes.AliceBlue;
                        case true:
                            actualJumpFrameIndex = 0;
                            switch (goingRight)
                            {
                                case true:
                                    //jobbra mozgó képkockák
                                    //return Brushes.Yellow;
                                    return RunBrush();
                                case false:
                                    return standRightBrush; //egy helyben áll
                                    //return Brushes.Orange;
                            }
                    }

                case false: //balra néz
                    switch (onFloor)
                    {
                        case false: //balra ugrik
                            return JumpBrush();
                            //return Brushes.Green;
                        case true:
                            actualJumpFrameIndex = 0;
                            switch (goingLeft)
                            {
                                case true:
                                    //balra mozgó képkockák
                                    return RunBrush();
                                    //return Brushes.Wheat;
                                case false:
                                    //return standLeftBrush; //egy helyben áll
                                    return standLeftBrush;
                            }
                    }
            }
        }
        private Brush RunBrush()
        {
            if(lookRight)
            {
                actualRunFrameIndex++;
                if (actualRunFrameIndex >= frameRunNumber)
                {
                    actualRunFrameIndex = 0;
                }
                return runRightBrushes[actualRunFrameIndex];
            }
            else
            {
                actualRunFrameIndex++;
                if (actualRunFrameIndex >= frameRunNumber)
                {
                    actualRunFrameIndex = 0;
                }
                return runLeftBrushes[actualRunFrameIndex];
            }
        }
        private Brush JumpBrush()
        {
            if(lookRight)
            {
                if (actualJumpFrameIndex < frameJumpNumber - 1)
                {
                    actualJumpFrameIndex++;
                    return jumpRightBrushes[actualJumpFrameIndex];
                }
                else
                {
                    return jumpRightBrushes[actualJumpFrameIndex - 1];
                }
            }
            else
            {
                if (actualJumpFrameIndex < frameJumpNumber - 1)
                {
                    actualJumpFrameIndex++;
                    return jumpLeftBrushes[actualJumpFrameIndex];
                }
                else
                {
                    return jumpLeftBrushes[actualJumpFrameIndex - 1];
                }
            }
        }
    }
}
