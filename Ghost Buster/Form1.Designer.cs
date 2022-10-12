namespace Ghost_Buster
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtAmmo = new System.Windows.Forms.Label();
            this.txtKills = new System.Windows.Forms.Label();
            this.txtHP = new System.Windows.Forms.Label();
            this.HPBar = new System.Windows.Forms.ProgressBar();
            this.player = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAmmo
            // 
            this.txtAmmo.AutoSize = true;
            this.txtAmmo.Font = new System.Drawing.Font("SimSun-ExtB", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmmo.Location = new System.Drawing.Point(16, 11);
            this.txtAmmo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtAmmo.Name = "txtAmmo";
            this.txtAmmo.Size = new System.Drawing.Size(101, 24);
            this.txtAmmo.TabIndex = 0;
            this.txtAmmo.Text = "Ammo: 0";
            this.txtAmmo.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtKills
            // 
            this.txtKills.AutoSize = true;
            this.txtKills.Font = new System.Drawing.Font("SimSun-ExtB", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKills.Location = new System.Drawing.Point(312, 11);
            this.txtKills.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtKills.Name = "txtKills";
            this.txtKills.Size = new System.Drawing.Size(114, 24);
            this.txtKills.TabIndex = 0;
            this.txtKills.Text = "Kills: 0";
            this.txtKills.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtHP
            // 
            this.txtHP.AutoSize = true;
            this.txtHP.Font = new System.Drawing.Font("SimSun-ExtB", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHP.Location = new System.Drawing.Point(602, 9);
            this.txtHP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtHP.Name = "txtHP";
            this.txtHP.Size = new System.Drawing.Size(49, 24);
            this.txtHP.TabIndex = 0;
            this.txtHP.Text = "HP:";
            this.txtHP.Click += new System.EventHandler(this.label1_Click);
            // 
            // HPBar
            // 
            this.HPBar.Location = new System.Drawing.Point(700, 9);
            this.HPBar.Margin = new System.Windows.Forms.Padding(4);
            this.HPBar.Name = "HPBar";
            this.HPBar.Size = new System.Drawing.Size(236, 28);
            this.HPBar.TabIndex = 1;
            this.HPBar.Value = 100;
            this.HPBar.Click += new System.EventHandler(this.HPBar_Click);
            // 
            // player
            // 
            this.player.Image = global::Ghost_Buster.Properties.Resources.up1;
            this.player.Location = new System.Drawing.Point(328, 293);
            this.player.Margin = new System.Windows.Forms.Padding(4);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(100, 100);
            this.player.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.player.TabIndex = 2;
            this.player.TabStop = false;
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.MainTimerEvent);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(986, 769);
            this.Controls.Add(this.player);
            this.Controls.Add(this.HPBar);
            this.Controls.Add(this.txtHP);
            this.Controls.Add(this.txtKills);
            this.Controls.Add(this.txtAmmo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Ghost Buster";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtAmmo;
        private System.Windows.Forms.Label txtKills;
        private System.Windows.Forms.Label txtHP;
        private System.Windows.Forms.ProgressBar HPBar;
        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.Timer gameTimer;
    }
}

