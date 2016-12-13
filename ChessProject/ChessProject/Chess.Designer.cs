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
            this.новаяИграToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.новаяИграToolStripMenuItem.Text = "Меню";
            // 
            // новаяИграToolStripMenuItem1
            // 
            this.новаяИграToolStripMenuItem1.Name = "новаяИграToolStripMenuItem1";
            this.новаяИграToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.новаяИграToolStripMenuItem1.Text = "Новая игра";
            this.новаяИграToolStripMenuItem1.Click += new System.EventHandler(this.новаяИграToolStripMenuItem1_Click);
            // 
            // сохранитьИгруToolStripMenuItem
            // 
            this.сохранитьИгруToolStripMenuItem.Name = "сохранитьИгруToolStripMenuItem";
            this.сохранитьИгруToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.сохранитьИгруToolStripMenuItem.Text = "Сохранить игру";
            this.сохранитьИгруToolStripMenuItem.Click += new System.EventHandler(this.сохранитьИгруToolStripMenuItem_Click);
            // 
            // продолжитьToolStripMenuItem
            // 
            this.продолжитьToolStripMenuItem.Name = "продолжитьToolStripMenuItem";
            this.продолжитьToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.продолжитьToolStripMenuItem.Text = "Продолжить";
            this.продолжитьToolStripMenuItem.Click += new System.EventHandler(this.продолжитьToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяИграToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1105, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip2";
            this.menuStrip.Visible = false;
            // 
            // Chess
            // 
            this.ClientSize = new System.Drawing.Size(1105, 721);
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
    }
}

