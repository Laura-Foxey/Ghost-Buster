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
        int playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int enemySpeed = 2;
        int kills = 0;
        Random random = new Random();

        List<PictureBox> enemyList = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (playerHealth > 0)
            {
                HPBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
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

            if (e.KeyCode == Keys.Space)
            {
                Shoot(facing);
            }
        }

        private void w(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.up;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                player.Image = Properties.Resources.down;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.left;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.right;
            }
        }

        private void Shoot(string dir)
        {

        }

        private void SpawnEnemy()
        {

        }

        private void Reset()
        {

        }
    }
}
