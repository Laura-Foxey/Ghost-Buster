using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                //make ghosts go towards player && change the picture to match direction
                //increase ghost speed based on kills
                if (c is PictureBox && (string)c.Tag == "ghost")
                {
                    if (kills > 15 ) { enemySpeed = 2; }
                    else if (kills > 30) {  enemySpeed = 3; }
           
                    if (c.Left > player.Left)
                    {
                        c.Left -= enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.zleft1;  //change this
                    }
                    if (c.Left < player.Left)
                    {
                        c.Left += enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.zright1;  //change this
                    }
                    if (c.Top > player.Top)
                    {
                        c.Top -= enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.zup1;  //change this
                    }
                    if (c.Top < player.Top)
                    {
                        c.Top += enemySpeed;
                        ((PictureBox)c).Image = Properties.Resources.zdown1;  //change this
                    }

                    //decrease player health when they are intersecting with the ghosts
                    if (player.Bounds.IntersectsWith(c.Bounds))
                    {
                        playerHealth -= 0.3;
                    }
                }

                //removes ghost and bullet icon on collision
                foreach (Control g in this.Controls)
                {
                    if (g is PictureBox && (string)g.Tag == "bullet" && c is PictureBox && (string)c.Tag == "ghost")
                    {
                        if (c.Bounds.IntersectsWith(g.Bounds))
                        {
                            kills++;
                            this.Controls.Remove(g);
                            this.Controls.Remove(c);
                            ((PictureBox)g).Dispose();
                            ((PictureBox)c).Dispose();
                            enemyList.Remove(((PictureBox)c));
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
