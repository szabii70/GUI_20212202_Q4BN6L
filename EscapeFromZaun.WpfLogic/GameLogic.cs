using EscapeFromZaun.Model;
using EscapeFromZaun.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Media;
using System.Threading;

namespace EscapeFromZaun.WpfLogic
{
    public class GameLogic : IGameLogic
    {
        IMapGeneratingRepository mapRepo;
        bool lookRight;
        bool lookLeft;
        int roofCord;
        bool goingLeft = false;
        bool goingRight = false;
        bool jumping;
        int force = 20;
        int jumpSpeed = 10;
        System.Windows.Size windowSize;

        public event EventHandler GameFinished;
        public event EventHandler GamePaused;

        public Brush PlayerBrush { get; set; }
        public PlayerModel Player { get; set; }


        public enum Directions { left, right }
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

        //ENEMY----------------------------------------------------------------------------------------------------------------------

        double enemySpeed = 2.5;
        public List<Enemy> Enemies { get; set; }
        //Collison
        bool collisonEnter = false;
        bool collisonLeft = false; //ha balrol erkezik a karakter 
        bool collisonRight = false; //ha jobbrol erkezik a karakter 
        //Attack
        int TimeCount = 0;

        public List<Bullet> Bullets { get; set; }

        //ENEMY-------------------------------------------------------------------------------------------------------------------------------
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

            BackgroundDatas = new BackgroundDatas(windowSize.Width, 10000) { DrawFromX = 0, DrawFromY = roofCord - 300 }; //Késõbb lehet a kép tetejét bõvítjük, ahelyett, hogymagasabbról kezdõdjön a kirajzolás
        }
        public GameLogic(IMapGeneratingRepository mapRepo)
        {
            this.mapRepo = mapRepo;
            Player = new PlayerModel()
            {
                Name = "",
                Outcome = "Victory",
                PlayerRunTime = new DateTime(0)
            };
        }


        public void SetupMap()
        {
            Platforms = new List<Platform>();
            Enemies = new List<Enemy>();
            Bullets = new List<Bullet>();
            string[] lines = mapRepo.MapInStringLines();

            int rowsNumber = lines.Length;
            int colsNumber = lines[0].Length;

            int platformWidth = (int)windowSize.Width / colsNumber;

            roofCord = (-1) * rowsNumber * 100; // in every 100 px a platform is drawn
            ;
            for (int i = 0; i < rowsNumber; i++)
            {
                for (int j = 0; j < colsNumber; j++)
                {
                    var tmp = lines[i];
                    var tmpC = tmp[j];
                    if (lines[i][j] == 'J')
                    {
                        MainPlayer = new PlayerLogic(350, 200, 10) { DrawFromX = j * platformWidth, DrawFromY = roofCord + i * 100 + 1000 }; //384 is platfrom width but it can also be calculated by windowWidth / 5
                    }
                    else if (lines[i][j] == 'P')
                    {
                        Platforms.Add(new Platform(j * platformWidth, roofCord + i * 100 + 1000, 384, 50));
                        ;
                    }
                    else if (lines[i][j] == 'B')
                    {
                        Platforms.Add(new Platform(j * platformWidth, roofCord + i * 100 + 1000, 2*384, 100));
                    }
                    else if (lines[i][j] == 'F')
                    {
                        Platforms.Add(new FinishPlatform(j * platformWidth, roofCord + i * 100 + 1000, 100, 100));
                    }
                    else if (lines[i][j] == 'E')
                    {
                        Enemies.Add(new Enemy(j * platformWidth, roofCord + i * 100 + 1000, 100, 200, 7));
                    }
                    else if (lines[i][j] == 'M')
                    {
                        Platforms.Add(new Platform(j * platformWidth, roofCord + i * 100 + 1000, 384, 50) { Moveable=true});
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
                    lookLeft = true;
                    break;
                case Key.D:
                    goingRight = true;
                    goingLeft = false;
                    lookRight = true;
                    lookLeft = false;
                    break;
                case Key.W:
                    Jump();
                    break;
                case Key.Space:
                    PlayerShoot();
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
                case Key.Escape:
                    EventHandler handler = GamePaused;
                    handler?.Invoke(this, null);
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
            PlatformsMove();
            EnemyInteract();
            PlayerInteract();
            BulletInteract();
            TimeCount++;
        }

        public void PlayerShoot() //UJ
        {

            MainPlayer.Damage = 2;
            if (lookLeft == true && lookRight == false) //Ha balra nez akkor arra lõ
            {
                Bullet bullet = new Bullet(new System.Drawing.Point((int)MainPlayer.Hitbox.Bounds.X - 60, (int)MainPlayer.Hitbox.Bounds.Y + 20), new System.Windows.Vector(-20, 0));

                bullet.Type = BulletType.player;
                SoundPlayer soundPlayer = new SoundPlayer("Views/Audio/Songs/piu2.wav");
                soundPlayer.Play();
                
                

                Bullets.Add(bullet);
                

                //Bullets.Add(new Bullet(new System.Drawing.Point((int)MainPlayer.Hitbox.Bounds.X - 60, 
                //    (int)MainPlayer.Hitbox.Bounds.Y + 20),
                //     new System.Windows.Vector(-20, 0)));
            }
            if (lookRight == true && lookLeft == false) //Ha jobbra nez akkor arra lõ
            {
                Bullet bullet = new Bullet(new System.Drawing.Point((int)MainPlayer.Hitbox.Bounds.X + 150, (int)MainPlayer.Hitbox.Bounds.Y + 20), new System.Windows.Vector(20, 0));

                bullet.Type = BulletType.player;
                SoundPlayer soundPlayer = new SoundPlayer("Views/Audio/Songs/piu2.wav");
                soundPlayer.Play();
                Bullets.Add(bullet);


                //Bullets.Add(new Bullet(new System.Drawing.Point((int)MainPlayer.Hitbox.Bounds.X + 150,
                //(int)MainPlayer.Hitbox.Bounds.Y + 20),
                //     new System.Windows.Vector(20, 0)));
            }

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
            foreach (var item in Enemies)
            {
                item.DrawFromY -= jumpSpeed;
            }
            foreach (var bullet in Bullets)
            {
                var point = (new System.Drawing.Point(bullet.Center.X, bullet.Center.Y - jumpSpeed));
                bullet.Center = point;
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

                //Enemy
                foreach (var enemy in Enemies)
                {
                    enemy.DrawFromY += force;
                }

                foreach (var bullet in Bullets)
                {
                    var point = (new System.Drawing.Point(bullet.Center.X, bullet.Center.Y + force));
                    bullet.Center = point;
                }
                //Bullet
                //foreach (var bullet in Bullets)
                //{
                //    bullet.Center = new System.Drawing.Point(bullet.Center.X + force, bullet.Center.Y);
                //}
               
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

                foreach (var enemy in Enemies)
                {
                    enemy.DrawFromY -= force;
                }

                foreach (var bullet in Bullets)
                {
                    var point = (new System.Drawing.Point(bullet.Center.X, bullet.Center.Y - force));
                    bullet.Center = point;
                }
                //foreach (var bullet in Bullets)
                //{
                //    bullet.Center = new System.Drawing.Point(bullet.Center.X - force, bullet.Center.Y);
                //}

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
                    if (item is FinishPlatform)
                    {
                        TimeCount = 0;
                        GameFinished(this, null);
                        
                    }
                }
                if (MainPlayer.IsCollision(item.Area) && item.Area.Bounds.Left >= MainPlayer.Hitbox.Bounds.Left && MainPlayer.Hitbox.Bounds.Bottom > item.Area.Bounds.Bottom) //bal oldalról ütközik miközben jobbra megy
                {
                    MainPlayer.Move(Directions.left);
                    if (item is FinishPlatform)
                    {
                        TimeCount = 0;
                        GameFinished(this, null);
                        
                    }
                }
                else if (MainPlayer.IsCollision(item.Area) && item.Area.Bounds.Right <= MainPlayer.Hitbox.Bounds.Right && MainPlayer.Hitbox.Bounds.Bottom > item.Area.Bounds.Bottom)
                {
                    MainPlayer.Move(Directions.right);
                    if (item is FinishPlatform)
                    {
                        TimeCount = 0;
                        GameFinished(this, null);
                        
                    }
                }
                if (MainPlayer.IsCollision(item.Area) && jumping == false && MainPlayer.Hitbox.Bounds.BottomLeft.Y <= item.Area.Bounds.BottomLeft.Y) //talajjal való ütközés
                {

                    if (item is FinishPlatform)
                    {
                        TimeCount = 0;
                        GameFinished?.Invoke(this, null);
                    }
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

                foreach (var enemy in Enemies)
                {
                    enemy.DrawFromY += dif;
                }

                foreach (var bullet in Bullets)
                {
                    var point = (new System.Drawing.Point(bullet.Center.X, bullet.Center.Y + dif));
                    bullet.Center = point;
                }

            }
            PlayerBrush = MainPlayer.PlayerBrush(lookRight, goingRight, goingLeft, onFloor);
            onFloor = false;
        }


        public void EnemyInteract()
        {
            bool onFloor = false;
            int dif = 0;

            foreach (var enemy in Enemies)
            {
                //MOZGAS------------------------------------------------------------------------------------------------------

                enemy.Damage = 1; // DAMAGE

                //enemy.DrawFromX -= (int)enemySpeed;

                //if (enemy.Area.Bounds.Left < platform.Area.Bounds.Left || enemy.Area.Bounds.Left + enemy.Area.Bounds.Width > platform.Area.Bounds.Left + platform.Area.Bounds.Width)
                //{
                //    enemySpeed = -enemySpeed;

                //}
                //-----------------------------------------------------------------------------------------------------------------

                //BAL
                //if (MainPlayer.IsCollision(enemy.CollisonArea) && enemy.CollisonArea.Bounds.Left >= MainPlayer.Hitbox.Bounds.Left && MainPlayer.Hitbox.Bounds.Bottom > enemy.CollisonArea.Bounds.Bottom)
                //{
                //    collisonEnter = true;
                //    collisonLeft = true;
                //    collisonRight = false;
                //    if (collisonEnter == true)
                //    {
                //        enemy.DrawFromX += (int)enemySpeed;
                //        enemySpeed = 0;
                //        Attack();
                //    }

                //    //CollisonExit();
                //}
                ////JOBB
                //else if (MainPlayer.IsCollision(enemy.CollisonArea) && enemy.CollisonArea.Bounds.Right <= MainPlayer.Hitbox.Bounds.Right && MainPlayer.Hitbox.Bounds.Bottom > enemy.CollisonArea.Bounds.Bottom)
                //{

                //    collisonEnter = true;
                //    collisonLeft = false;
                //    collisonRight = true;
                //    if (collisonEnter == true)
                //    {
                //        enemy.DrawFromX += (int)enemySpeed;
                //        enemySpeed = 0;
                //        Attack();
                //    }

                //    //CollisonExit();

                //}

                if (TimeCount == 40)
                {

                    if ((enemy.Area.Bounds.X - MainPlayer.Hitbox.Bounds.X) < 0)
                    {
                        collisonRight = true;
                        collisonLeft = false;
                        Attack();
                    }
                    if ((enemy.Area.Bounds.X - MainPlayer.Hitbox.Bounds.X) > 0)
                    {
                        collisonRight = false;
                        collisonLeft = true;
                        Attack();
                    }
                    TimeCount = 0;
                }




                //player collison with enemy

                if (MainPlayer.IsCollision(enemy.Area) && enemy.Area.Bounds.Left >= MainPlayer.Hitbox.Bounds.Left
                    && MainPlayer.Hitbox.Bounds.Bottom > enemy.Area.Bounds.Bottom) //bal oldalról ütközik miközben jobbra megy
                {
                    MainPlayer.Move(Directions.left);

                }
                else if (MainPlayer.IsCollision(enemy.Area) && enemy.Area.Bounds.Right <= MainPlayer.Hitbox.Bounds.Right && MainPlayer.Hitbox.Bounds.Bottom > enemy.Area.Bounds.Bottom)
                {
                    MainPlayer.Move(Directions.right);
                }
                if (MainPlayer.IsCollision(enemy.Area) && jumping == false && MainPlayer.Hitbox.Bounds.BottomLeft.Y <= enemy.Area.Bounds.BottomLeft.Y) //talajjal való ütközés
                {
                    force = 20; //it was 25
                                //MainPlayer.DrawFromY = (int)enemy.Area.Bounds.TopLeft.Y - (int)MainPlayer.Hitbox.Bounds.Height;
                    onFloor = true;
                    dif = (int)(MainPlayer.Hitbox.Bounds.Bottom + enemy.Area.Bounds.Height - enemy.Area.Bounds.Bottom);
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

                foreach (var enemy in Enemies)
                {
                    enemy.DrawFromY += dif;
                }
            }
            PlayerBrush = MainPlayer.PlayerBrush(lookRight, goingRight, goingLeft, onFloor);
            onFloor = false;
        }


        public void BulletInteract() // Enemy golyo interakcio a playerrol
        {

            try
            {
                for (int i = 0, k = 0; i < Bullets.Count; i++, k++)
                {

                    Rect bulletRect = new Rect(Bullets[i].Center.X - 3, Bullets[i].Center.Y - 3, 5, 5);
                    bool inside = Bullets[i].Move(new System.Windows.Size((int)windowSize.Width, (int)windowSize.Height));
                    if (!inside)
                    {

                        Bullets.RemoveAt(k);

                    }
                    else
                    {

                        for (int j = 0; j < Enemies.Count; j++)
                        {

                            //Rect bulletRect = new Rect(Bullets[i].Center.X - 3, Bullets[i].Center.Y - 3, 5, 5);
                            if (bulletRect.IntersectsWith(Enemies[j].Area.Bounds) && Bullets[i].Type == BulletType.player)
                            {
                                Bullets.RemoveAt(k);
                                Enemies[j].Health -= MainPlayer.Damage;
                                ;
                                if (Enemies[j].Health < 0)
                                {
                                    ;
                                    Enemies.RemoveAt(j);

                                }
                            }
                            else if (bulletRect.IntersectsWith(MainPlayer.Hitbox.Bounds) && Bullets[i].Type == BulletType.enemy)
                            {
                                Bullets.RemoveAt(k);
                                MainPlayer.Health -= Enemies[j].Damage;
                                if (MainPlayer.Health < 0)
                                {
                                    TimeCount = 0;
                                    Player.Outcome = "Defeat";
                                    GameFinished(this, null);
                                }

                            }

                        }




                    }

                }
            }
            catch (Exception)
            {

                
            }
           
        }


        public void Attack()
        {
            ;
            //LookAtTarget();
            //Majd pedig a tamadas a player fele egyenlore skip

            for (int i = 0; i < Enemies.Count; i++)
            {

                if (collisonLeft == true && collisonRight == false)
                {
                    Bullet bullet = new Bullet(new System.Drawing.Point((int)Enemies[i].Area.Bounds.X - 60, (int)Enemies[i].Area.Bounds.Y + 75), new System.Windows.Vector(-20, 0));
                    bullet.Type = BulletType.enemy;
                    Bullets.Add(bullet);

                    
                    //loves balra

                }
                else if (collisonRight == true && collisonLeft == false)
                {
                    Bullet bullet = new Bullet(new System.Drawing.Point((int)Enemies[i].Area.Bounds.X + 150, (int)Enemies[i].Area.Bounds.Y + 75), new System.Windows.Vector(20, 0));
                    
                    bullet.Type = BulletType.enemy;
                    Bullets.Add(bullet);

                    
                    // loves jobbra
                }



            }


        }
        private void PlatformsMove()
        {
            foreach (var platform in Platforms)
            {
                if(platform.Moveable)
                {
                    if(platform.GoingRight == true && platform.Area.Bounds.Right < windowSize.Width)
                    {
                        platform.DrawFromX += 3;
                    }
                    else
                    {
                        platform.GoingRight = false;
                    }
                    if(platform.GoingRight == false && platform.Area.Bounds.Left >= 0)
                    {
                        platform.DrawFromX -= 3;
                    }
                    else
                    {
                        platform.GoingRight = true;
                    }
                }
            }
        }

    }
}
