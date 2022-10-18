using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Ghost_Buster
{
    public partial class Form1 : Form
    {
        bool goUp, goDown, goLeft, goRight, gameOver = false;
        string facing = "up";
        double playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int enemySpeed = 1;
        int kills = 0;
        Random random = new Random();
        static int[] enemySpawnArray1 = { 15, 30, 45, 60, 75, 90 };
        static int[] enemySpawnArray2 = { 90, 100, 110, 120, 130, 140};
        static int[] enemySpawnArray3 = Enumerable.Range(30, 50).Select(x => x * 5).ToArray();

        int[] enemySpawnArr = enemySpawnArray1.Concat(enemySpawnArray2).Concat(enemySpawnArray3).ToArray();

        List<PictureBox> enemyList = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
            Reset();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            foreach (int x in enemySpawnArr)
            {
                Console.WriteLine(x);
            }

            if (playerHealth > 0)
            {
                HPBar.Value = (int)playerHealth;
                txtHP.Text = "HP: " + playerHealth;
            }
            else
            {
                gameOver = true;
                player.Image = Properties.Resources.dead1;
                gameTimer.Stop();
                txtHP.Text = "HP: 0";
            }

            txtAmmo.Text = "Ammo: " + ammo;
            txtKills.Text = "Kills: " + kills;

            //change direction speed depending on the button pressed & limit to the size of window
            if (goUp == true && player.Top > 40)
            {
                player.Top -= speed;
            }
            if (goDown == true && player.Top + player.Height < ClientSize.Height)
            {
                player.Top += speed;
            }
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= speed;
            }
            if (goRight == true && player.Left + player.Width < ClientSize.Width)
            {
                player.Left += speed;
            }

            foreach (Control c in this.Controls)
            {
                //make player interact with ammo
                if (c is PictureBox && (string)c.Tag == "ammo" )
                {
                    if (player.Bounds.IntersectsWith(c.Bounds))
                    {
                        ammo += 5;
                        c.Dispose();
                    }
                }

                if (c is PictureBox && (string)c.Tag == "health")
                {
                    if (player.Bounds.IntersectsWith(c.Bounds))
                    {
                        playerHealth += 10;
                        c.Dispose();
                    }
                }

                //make ghosts go towards player && change the picture to match direction
                //increase ghost speed based on kills
                if (c is PictureBox && (string)c.Tag == "ghost")
                {
                    if (kills > 15 ) { enemySpeed = 2; }
                    else if (kills > 30) {  enemySpeed = 3; }
           
                    if (c.Left > player.Left)
                    {
                        c.Left -= enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.zleft1;
                    }
                    if (c.Left < player.Left)
                    {
                        c.Left += enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.zright1;  
                    }
                    if (c.Top > player.Top)
                    {
                        c.Top -= enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.zup1;  
                    }
                    if (c.Top < player.Top)
                    {
                        c.Top += enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.zdown1;  
                    }

                    //decrease player health when they are intersecting with the ghosts
                    if (player.Bounds.IntersectsWith(c.Bounds))
                    {
                        playerHealth -= 0.3;
                    }
                }

                //same as top but different type of enemy
                if (c is PictureBox && (string)c.Tag == "redGhost")
                {
                    enemySpeed = 3;
                    if (c.Left > player.Left)
                    {
                        c.Left -= enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.z2left;
                    }
                    if (c.Left < player.Left)
                    {
                        c.Left += enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.z2right;
                    }
                    if (c.Top > player.Top)
                    {
                        c.Top -= enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.z2up;
                    }
                    if (c.Top < player.Top)
                    {
                        c.Top += enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.z2down;
                    }

                    //decrease player health when they are intersecting with the ghosts
                    if (player.Bounds.IntersectsWith(c.Bounds))
                    {
                        playerHealth -= 0.5;
                    }
                }

                //removes ghost and bullet icon on collision
                foreach (Control g in this.Controls)
                {
                    if (g is PictureBox && (string)g.Tag == "bullet" && c is PictureBox && ((string)c.Tag == "ghost" || (string)c.Tag == "redGhost"))
                    {
                        if (c.Bounds.IntersectsWith(g.Bounds))
                        {
                            kills++;
                            this.Controls.Remove(g);
                            this.Controls.Remove(c);
                            ((PictureBox)g).Dispose();
                            ((PictureBox)c).Dispose();
                            enemyList.Remove(((PictureBox)c));
                            if (enemySpawnArr.Contains(kills))
                            {
                                SpawnHarderEnemy();
                            }
                            else 
                            SpawnEnemy();
                        }
                    }
                }
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Space && ammo > 0 && gameOver == false)
            {
                ammo--;
                Shoot(facing);
                if (ammo < 1)
                {
                    SpawnAmmo();
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver == false)
            {
                if (e.KeyCode == Keys.Up)
                {
                    goUp = true;
                    facing = "up";
                    player.Image = Properties.Resources.up1;
                }
                if (e.KeyCode == Keys.Down)
                {
                    goDown = true;
                    facing = "down";
                    player.Image = Properties.Resources.down1;
                }
                if (e.KeyCode == Keys.Left)
                {
                    goLeft = true;
                    facing = "left";
                    player.Image = Properties.Resources.left1;
                }
                if (e.KeyCode == Keys.Right)
                {
                    goRight = true;
                    facing = "right";
                    player.Image = Properties.Resources.right1;
                }
            }
            if (e.KeyCode == Keys.Space && gameOver == true)
            {
                Reset();
            }
        }

        private void HPBar_Click(object sender, EventArgs e)
        {

        }

        private void Shoot(string dir)
        {
            Bullet shoot = new Bullet();
            shoot.dir = dir;

            //bullet originates from the barrel
            if (dir == "down")
            {
                shoot.bulletLeft = player.Left + (player.Width / 2) - 30;
                shoot.bulletTop = player.Top + (player.Height / 2);
            }
            if (dir == "up")
            {
                shoot.bulletLeft = player.Left + (player.Width / 2) + 30;
                shoot.bulletTop = player.Top + (player.Height / 2);
            }
            if (dir == "right")
            {
                shoot.bulletLeft = player.Left + (player.Width / 2);
                shoot.bulletTop = player.Top + (player.Height / 2) + 30;
            }
            if (dir == "left")
            {
                shoot.bulletLeft = player.Left + (player.Width / 2);
                shoot.bulletTop = player.Top + (player.Height / 2) - 30;
            }

            shoot.CreateBullet(this);
        }

        private void SpawnEnemy()
        {
            PictureBox ghost = new PictureBox();
            ghost.Tag = "ghost";
            ghost.Image = Properties.Resources.zdown1;  //TODO: changethis
            ghost.Left = random.Next(0, 900);
            ghost.Top = random.Next(0, 800);
            ghost.SizeMode = PictureBoxSizeMode.AutoSize;
            enemyList.Add(ghost);

            this.Controls.Add(ghost);

            //makes sure the player img does not get overlapped 
            player.BringToFront();
        }

        //spawns health every 10 seconds if the odds are with you :)
        private void HealthTimerEvent(object sender, EventArgs e)
        {
            int randomNum = random.Next(1, 100);
            if (randomNum % 2 == 0)
            { SpawnHealth(); }
        }

        private void SpawnHarderEnemy()
        {
            PictureBox redGhost = new PictureBox();
            redGhost.Tag = "redGhost";
            redGhost.Image = Properties.Resources.z2down;  //TODO: changethis
            redGhost.Left = random.Next(0, 900);
            redGhost.Top = random.Next(0, 800);
            redGhost.SizeMode = PictureBoxSizeMode.AutoSize;
            enemyList.Add(redGhost);

            this.Controls.Add(redGhost);

            //makes sure the player img does not get overlapped 
            player.BringToFront();
        }

        private void SpawnAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Tag = "ammo";
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = random.Next(10, ClientSize.Width - ammo.Width);
            ammo.Top = random.Next(10, ClientSize.Height - ammo.Height);

            this.Controls.Add(ammo);

            //makes sure the ammo and player img does not get overlapped 
            ammo.BringToFront();
            player.BringToFront();
        }

        private void SpawnHealth()
        {
            PictureBox health = new PictureBox();
            health.Tag = "health";
            health.Image = Properties.Resources.health_Image;
            health.SizeMode = PictureBoxSizeMode.AutoSize;
            health.Left = random.Next(10, ClientSize.Width - health.Width);
            health.Top = random.Next(10, ClientSize.Height - health.Height);

            this.Controls.Add(health);

            //makes sure the ammo and player img does not get overlapped 
            health.BringToFront();
            player.BringToFront();
        }

        //resets the game with default values
        private void Reset()
        {
            player.Image = Properties.Resources.up1;

            foreach(PictureBox pic in enemyList)
            {
                this.Controls.Remove(pic);
            }

            //clears current enemies and spawn 
            enemyList.Clear();

            for (int i = 0; i < 3; i++)
            {
                SpawnEnemy();
            }

            enemySpeed = 2;
            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            playerHealth = 100;
            ammo = 10;
            kills = 0;

            gameOver = false;
            gameTimer.Start();
        }
    }
}
