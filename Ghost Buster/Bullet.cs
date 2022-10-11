using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Ghost_Buster
{
    internal class Bullet
    {
        public string dir;
        public int bulletTop;
        public int bulletLeft;
        private int speed = 20;
        private PictureBox bullet = new PictureBox();
        private Timer bulletTimer = new Timer();

        public void CreateBullet(Form arg)
        {
            bullet.BackColor = Color.White;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;
            bullet.BringToFront();

            //add the arguments for bullet to the form
            arg.Controls.Add(bullet);

            bulletTimer.Interval = speed;
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();
        }

        private void BulletTimerEvent(object sender, EventArgs e)
        {
            //launches bullet in the direction character is facing
            if (dir == "up")
            {
                bullet.Top -= speed;
            }
            if (dir == "down")
            {
                bullet.Top += speed;
            }
            if (dir == "left")
            {
                bullet.Left -= speed;
            }
            if (dir == "right")
            {
                bullet.Left += speed;
            }

            //disposes the bullet if it goes outside bounding box of window
            if (bullet.Left < 5 || bullet.Left > 940 || bullet.Top < 5 || bullet.Top > 800)
            {
                bulletTimer.Stop();
                bulletTimer.Dispose();
                bullet.Dispose();
                bullet = null;
                bulletTimer = null;
            }

        }
    }
}
