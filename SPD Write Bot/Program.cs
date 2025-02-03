using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPD_Write_Bot
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
            SPD Frm1 = new SPD();
            Frm1.Left = int.Parse(ConfigurationManager.AppSettings["PosX"]);
            Frm1.Top = int.Parse(ConfigurationManager.AppSettings["PosY"]) ;
            Application.Run(Frm1);
        }
    }
}






