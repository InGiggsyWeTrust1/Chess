using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ChessProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Chess());
        }
    }
}
