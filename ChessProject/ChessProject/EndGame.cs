using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProject
{
    public partial class EndGame : Form
    {
        public EndGame()
        {
            InitializeComponent();
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            Application.Restart();

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
