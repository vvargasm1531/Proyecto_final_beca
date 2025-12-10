using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proyecto_final_beca
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //https://aka.ms/applicationconfiguration.

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Loggin());
        }
    }
}
