using EscapeFromZaun.Model;
using EscapeFromZaun.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Media;

namespace EscapeFromZaun.WpfLogic
{
    public class GameLogic : IGameLogic
    {
        IMapGeneratingRepository mapRepo;
        bool lookRight;
        int roofCord;
        bool goingLeft = false;
        bool goingRight = false;
        bool jumping;
        int force = 20;
        int jumpSpeed = 10;
        System.Windows.Size windowSize;

        public event EventHandler GameFinished;
        public Brush PlayerBrush { get; set; }
        public PlayerModel Player { get; set; }

        public GameLogic()
        {
            Player = new PlayerModel()
            {
                Name = "",
                Outcome = "Victory",
                PlayerRunTime = DateTime.Now
            };
        }
        public enum Directions { left,right }
        public PlayerLogic MainPlayer { get; set; }
        public List<Platform> Platforms { get; set; }
        public BackgroundDatas BackgroundDatas { get; set; }
        public Rect BackgroundRect
        {
            get
            {
                return new Rect(BackgroundDatas.DrawFromX, BackgroundDatas.DrawFromY, windowSize.Width, 10000);
            }
        }


        public void SetupSizes(System.Windows.Size size)
        {
            this.windowSize = size;
            //MainPlayer = new PlayerLogic(200, 200) { DrawFromX = 300, DrawFromY = 600 }; // interface-ként vegyük át

            //Platforms = new List<Platform>();
            //Platforms.Add(new Platform(0, 930, (int)windowSize.Width, 50)); //ezek csak próba platformok, majd erre külön platform generáló metódust hozunk létre
            //Platforms.Add(new Platform(500, 800, 300, 50));
            //Platforms.Add(new Platform(1000, 650, 300, 50));
            //Platforms.Add(new Platform(1500, 500, 300, 50));
            //Platforms.Add(new Platform(800, 300, 300, 50));
            //Platforms.Add(new Platform(500, 100, 300, 50));
            //Platforms.Add(new Platform(100, -100, 300, 50));
            //Platforms.Add(new Platform(700, -300, 300, 50));
            SetupMap();
            BackgroundDatas = new BackgroundDatas(windowSize.Width, 10000) { DrawFromX=0, DrawFromY=roofCord-300}; //Késõbb lehet a kép tetejét bõvítjük, ahelyett, hogymagasabbról kezdõdjön a kirajzolás
        }
        public GameLogic(IMapGeneratingRepository mapRepo)
        {
            this.mapRepo = mapRepo;
        }


        public void SetupMap()
        {
            Platforms = new List<Platform>();
            string[] lines = mapRepo.MapInStringLines();

            int rowsNumber = lines.Length;
            int colsNumber = lines[0].Length;

            int platformWidth = (int)windowSize.Width/colsNumber;

            roofCord = (-1)*rowsNumber * 100; // in every 100 px a platform is drawn
            ;
            for (int i = 0; i < rowsNumber; i++)
            {
                for (int j = 0; j < colsNumber; j++)
                {
                    var tmp = lines[i];
                    var tmpC = tmp[j];
                    if(lines[i][j]=='J')
                    {
                        MainPlayer = new PlayerLogic(350, 200) { DrawFromX= j* platformWidth, DrawFromY = roofCord + i * 100 + 1000 }; //384 is platfrom width but it can also be calculated by windowWidth / 5
                    }
                    else if(lines[i][j]=='P')
                    {
                        Platforms.Add(new Platform(j * platformWidth, roofCord + i * 100 + 1000, 384, 50));
                        ;
                    }
                    else if(lines[i][j]=='B')
                    {
                        //not defined yet
                    }
                    else if(lines[i][j]=='F')
                    {
                        Platforms.Add(new FinishPlatform(j * platformWidth, roofCord + i * 100 + 1000, 100, 100));
                    }
                    else if(lines[i][j]=='E')
                    {
                        //not defined yet
                    }
                }
            }
        }

        public void Move(Key key)
        {
            switch (key)
            {
                case Key.A:
                    goingLeft = true;
                    goingRight = false;
                    lookRight = false;
                    break;
                case Key.D:
                    goingRight = true;
                    goingLeft = false;
                    lookRight = true;
                    break;
                case Key.W:
                    Jump();
                    break;
            }
        }

        public void NotMove(Key key)
        {
            switch (key)
            {
                case Key.A:
                    goingLeft = false;
                    break;
                case Key.D:
                    goingRight = false;
                    break;
            }
        }
        private void Jump()
        {
            if (force == 20) //it was 25
                jumping = true;
        }

        public void TimeStep()
        {
            PlayerInteract();
        }

        private void PlayerInteract()
        {
            if (goingLeft && MainPlayer.Hitbox.Bounds.TopLeft.X >= 0)
            {
                MainPlayer.Move(Directions.left);
            }
            else if (goingRight && MainPlayer.Hitbox.Bounds.TopRight.X <= windowSize.Width)
            {
                MainPlayer.Move(Directions.right);
            }
            foreach (var item in Platforms)
            {
                item.DrawFromY -= jumpSpeed;
            }
            BackgroundDatas.DrawFromY -= jumpSpeed;

            if (jumping == true)
            {
                jumpSpeed = -10;
                force -= 1;

                BackgroundDatas.DrawFromY += force;

                foreach (var item in Platforms)
                {
                    item.DrawFromY += force;
                }
            }
            else
            {
                jumpSpeed = 10;
                force += 1;

                BackgroundDatas.DrawFromY -= force;

                foreach (var item in Platforms)
                {
                    item.DrawFromY -= force;
                }
            }
            if (jumping == true && force <= 0)
            {
                jumping = false;
            }
            bool onFloor = false;
            int dif = 0;
            foreach (var item in Platforms)
            {
                if (MainPlayer.IsCollision(item.Area) && MainPlayer.Hitbox.Bounds.BottomLeft.Y >= item.Area.Bounds.BottomLeft.Y) //feje ütközik a plafonnal
                {
                    jumping = false;
                    if (item is FinishPlatform) GameFinished(this,null);
                }
                if (MainPlayer.IsCollision(item.Area) && item.Area.Bounds.Left >= MainPlayer.Hitbox.Bounds.Left && MainPlayer.Hitbox.Bounds.Bottom > item.Area.Bounds.Bottom) //bal oldalról ütközik miközben jobbra megy
                {
                    MainPlayer.Move(Directions.left);
                    if (item is FinishPlatform) GameFinished(this, null);
                }
                else if (MainPlayer.IsCollision(item.Area) && item.Area.Bounds.Right <= MainPlayer.Hitbox.Bounds.Right && MainPlayer.Hitbox.Bounds.Bottom > item.Area.Bounds.Bottom)
                {
                    MainPlayer.Move(Directions.right);
                    if (item is FinishPlatform) GameFinished(this, null);
                }
                if (MainPlayer.IsCollision(item.Area) && jumping == false && MainPlayer.Hitbox.Bounds.BottomLeft.Y <= item.Area.Bounds.BottomLeft.Y) //talajjal való ütközés
                {

                    if (item is FinishPlatform) GameFinished(this, null);
                    force = 20;
                    onFloor = true;
                    dif = (int)(MainPlayer.Hitbox.Bounds.Bottom + item.Area.Bounds.Height - item.Area.Bounds.Bottom);
                    jumpSpeed = 0;
                }
            }
            if (onFloor)
            {
                BackgroundDatas.DrawFromY += dif;
                foreach (var item in Platforms)
                {
                    item.DrawFromY += dif;
                }
            }
            PlayerBrush = MainPlayer.PlayerBrush(lookRight, goingRight, goingLeft, onFloor);
            onFloor = false;
        }
    }
}
