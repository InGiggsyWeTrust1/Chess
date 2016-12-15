namespace ChessProject
{
    partial class Chess
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
            this.startGameButton = new System.Windows.Forms.Button();
            this.figureCourses = new System.Windows.Forms.TextBox();
            this.новаяИграToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новаяИграToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьИгруToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.продолжитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.onlineButton = new System.Windows.Forms.Button();
            this.userName = new System.Windows.Forms.TextBox();
            this.ipServer = new System.Windows.Forms.TextBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.ipServerLabel = new System.Windows.Forms.Label();
            this.connectionButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // startGameButton
            // 
            this.startGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startGameButton.Location = new System.Drawing.Point(839, 51);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(157, 52);
            this.startGameButton.TabIndex = 0;
            this.startGameButton.Text = "Start Game";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // figureCourses
            // 
            this.figureCourses.BackColor = System.Drawing.Color.White;
            this.figureCourses.Location = new System.Drawing.Point(839, 116);
            this.figureCourses.Multiline = true;
            this.figureCourses.Name = "figureCourses";
            this.figureCourses.ReadOnly = true;
            this.figureCourses.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.figureCourses.Size = new System.Drawing.Size(157, 460);
            this.figureCourses.TabIndex = 1;
            this.figureCourses.Visible = false;
            // 
            // новаяИграToolStripMenuItem
            // 
            this.новаяИграToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяИграToolStripMenuItem1,
            this.сохранитьИгруToolStripMenuItem,
            this.продолжитьToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.новаяИграToolStripMenuItem.Name = "новаяИграToolStripMenuItem";
            this.новаяИграToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.новаяИграToolStripMenuItem.Text = "Меню";
            // 
            // новаяИграToolStripMenuItem1
            // 
            this.новаяИграToolStripMenuItem1.Name = "новаяИграToolStripMenuItem1";
            this.новаяИграToolStripMenuItem1.Size = new System.Drawing.Size(225, 30);
            this.новаяИграToolStripMenuItem1.Text = "Новая игра";
            this.новаяИграToolStripMenuItem1.Click += new System.EventHandler(this.новаяИграToolStripMenuItem1_Click);
            // 
            // сохранитьИгруToolStripMenuItem
            // 
            this.сохранитьИгруToolStripMenuItem.Name = "сохранитьИгруToolStripMenuItem";
            this.сохранитьИгруToolStripMenuItem.Size = new System.Drawing.Size(225, 30);
            this.сохранитьИгруToolStripMenuItem.Text = "Сохранить игру";
            this.сохранитьИгруToolStripMenuItem.Click += new System.EventHandler(this.сохранитьИгруToolStripMenuItem_Click);
            // 
            // продолжитьToolStripMenuItem
            // 
            this.продолжитьToolStripMenuItem.Name = "продолжитьToolStripMenuItem";
            this.продолжитьToolStripMenuItem.Size = new System.Drawing.Size(225, 30);
            this.продолжитьToolStripMenuItem.Text = "Продолжить";
            this.продолжитьToolStripMenuItem.Click += new System.EventHandler(this.продолжитьToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(225, 30);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяИграToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1105, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip2";
            this.menuStrip.Visible = false;
            // 
            // onlineButton
            // 
            this.onlineButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.onlineButton.Location = new System.Drawing.Point(307, 51);
            this.onlineButton.Name = "onlineButton";
            this.onlineButton.Size = new System.Drawing.Size(157, 52);
            this.onlineButton.TabIndex = 4;
            this.onlineButton.Text = "Online";
            this.onlineButton.UseVisualStyleBackColor = true;
            this.onlineButton.Click += new System.EventHandler(this.onlineButton_Click);
            // 
            // userName
            // 
            this.userName.BackColor = System.Drawing.Color.White;
            this.userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.userName.Location = new System.Drawing.Point(417, 164);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(201, 39);
            this.userName.TabIndex = 5;
            this.userName.Visible = false;
            this.userName.TextChanged += new System.EventHandler(this.userName_TextChanged);
            // 
            // ipServer
            // 
            this.ipServer.BackColor = System.Drawing.Color.White;
            this.ipServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ipServer.Location = new System.Drawing.Point(417, 223);
            this.ipServer.Name = "ipServer";
            this.ipServer.Size = new System.Drawing.Size(201, 39);
            this.ipServer.TabIndex = 6;
            this.ipServer.Visible = false;
            this.ipServer.TextChanged += new System.EventHandler(this.ipServer_TextChanged);
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.userNameLabel.Location = new System.Drawing.Point(171, 170);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(240, 29);
            this.userNameLabel.TabIndex = 7;
            this.userNameLabel.Text = "Имя пользователя:";
            this.userNameLabel.Visible = false;
            // 
            // ipServerLabel
            // 
            this.ipServerLabel.AutoSize = true;
            this.ipServerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ipServerLabel.Location = new System.Drawing.Point(219, 229);
            this.ipServerLabel.Name = "ipServerLabel";
            this.ipServerLabel.Size = new System.Drawing.Size(141, 29);
            this.ipServerLabel.TabIndex = 8;
            this.ipServerLabel.Text = "IP сервера:";
            this.ipServerLabel.Visible = false;
            // 
            // connectionButton
            // 
            this.connectionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectionButton.Location = new System.Drawing.Point(307, 326);
            this.connectionButton.Name = "connectionButton";
            this.connectionButton.Size = new System.Drawing.Size(157, 52);
            this.connectionButton.TabIndex = 9;
            this.connectionButton.Text = "Connection";
            this.connectionButton.UseVisualStyleBackColor = true;
            this.connectionButton.Visible = false;
            this.connectionButton.Click += new System.EventHandler(this.connectionButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(351, 626);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // Chess
            // 
            this.ClientSize = new System.Drawing.Size(1105, 721);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.connectionButton);
            this.Controls.Add(this.ipServerLabel);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.ipServer);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.onlineButton);
            this.Controls.Add(this.figureCourses);
            this.Controls.Add(this.startGameButton);
            this.Controls.Add(this.menuStrip);
            this.Name = "Chess";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.TextBox figureCourses;
        private System.Windows.Forms.ToolStripMenuItem новаяИграToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новаяИграToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьИгруToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem продолжитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Button onlineButton;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TextBox ipServer;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label ipServerLabel;
        private System.Windows.Forms.Button connectionButton;
        private System.Windows.Forms.Label label1;
    }
}

