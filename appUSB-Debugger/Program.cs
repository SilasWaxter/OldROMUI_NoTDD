using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using USBClassLibrary;


namespace appUSB_Debugger
{
    static class Program
    {
        [STAThread]
        static void Main()
        {                 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form1 = new Form1();
            Application.Run(form1);
        }
    }
}