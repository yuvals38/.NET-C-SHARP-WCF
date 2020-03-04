using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RIPEngine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.Sleep(2000);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RIPTask());
        }
    }
}