using EscapeFromZaun.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace EscapeFromZaun.WpfLogic
{
    public class GameLogic : IGameLogic
    {
        bool goingLeft = false;
        bool goingRight = false;
        bool jumping;
        int force = 20;
        int jumpSpeed = 10;
        Size windowSize;
        public PlayerModel Player { get; set; }
        public enum Directions { left,right }
        public PlayerLogic MainPlayer { get; set; }
        public List<Platform> Platforms { get; set; }

        public void SetupSizes(Size size)
        {
            this.windowSize = size;
            MainPlayer = new PlayerLogic(200, 200) { DrawFromX = 300, DrawFromY = 600 }; // interface-ként vegyük át

            Platforms = new List<Platform>();
            Platforms.Add(new Platform(0, 930, (int)windowSize.Width, 50)); //ezek csak próba platformok, majd erre külön platform generáló metódust hozunk létre
            Platforms.Add(new Platform(500, 800, 300, 50));
            Platforms.Add(new Platform(1000, 650, 300, 50));
            Platforms.Add(new Platform(1500, 500, 300, 50));
            Platforms.Add(new Platform(800, 300, 300, 50));
            Platforms.Add(new Platform(500, 100, 300, 50));
            Platforms.Add(new Platform(100, -100, 300, 50));
            Platforms.Add(new Platform(700, -300, 300, 50));
        }
        public GameLogic()
        {

        }
        public void Move(Key key)
        {
            switch (key)
            {
                case Key.A:
                    goingLeft = true;
                    goingRight = false;
                    break;
                case Key.D:
                    goingRight = true;
                    goingLeft = false;
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

            if (jumping == true)
            {
                jumpSpeed = -10;

                force -= 1;

                foreach (var item in Platforms)
                {
                    item.DrawFromY += force;
                }
            }
            else
            {
                jumpSpeed = 10;
                force += 1;

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
                }
                if (MainPlayer.IsCollision(item.Area) && item.Area.Bounds.Left >= MainPlayer.Hitbox.Bounds.Left && MainPlayer.Hitbox.Bounds.Bottom > item.Area.Bounds.Bottom) //bal oldalról ütközik miközben jobbra megy
                {
                    MainPlayer.Move(Directions.left);
                }
                else if (MainPlayer.IsCollision(item.Area) && item.Area.Bounds.Right <= MainPlayer.Hitbox.Bounds.Right && MainPlayer.Hitbox.Bounds.Bottom > item.Area.Bounds.Bottom)
                {
                    MainPlayer.Move(Directions.right);
                }
                if (MainPlayer.IsCollision(item.Area) && jumping == false && MainPlayer.Hitbox.Bounds.BottomLeft.Y <= item.Area.Bounds.BottomLeft.Y) //talajjal való ütközés
                {
                    force = 20;

                    onFloor = true;
                    dif = (int)(MainPlayer.Hitbox.Bounds.Bottom + item.Area.Bounds.Height - item.Area.Bounds.Bottom);
                    jumpSpeed = 0;
                }
            }
            if (onFloor)
            {
                foreach (var item in Platforms)
                {
                    item.DrawFromY += dif;
                }
            }
            onFloor = false;
        }
    }
}
