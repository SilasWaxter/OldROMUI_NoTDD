using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using USBClassLibrary;
using System.IO.Ports;

namespace appUSB_Debugger
{
    public partial class Form1 : Form
    {
        public USBClass LibraryUSB;
        public USBCustom USB;

        public Form1()
        {
            //CLASSES-OBJECTS
            LibraryUSB = new USBClass();
            USB = new USBCustom();
            
            var aes128 = new AES128Encryption();

            //BEHAVIOUR-FUNCTIONALITY
            aes128.GenerateKey();
            System.Diagnostics.Debug.WriteLine("");

            USB.SetupUSBForEvents(LibraryUSB, this);
            Serial.InitAllPorts(USB);


            //EVENTS
            LibraryUSB.RegisterForDeviceChange(true, this.Handle);      //For USBClass Library Events

            //TASKS

            //FORMS
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            bool IsHandled = false;

            LibraryUSB.ProcessWindowsMessage(m.Msg, m.WParam, m.LParam, ref IsHandled);

            base.WndProc(ref m);
        }               //For USBClass Library Events
    }
}
