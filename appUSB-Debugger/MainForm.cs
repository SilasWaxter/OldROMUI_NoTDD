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
    public partial class MainForm : Form
    {
        public USBClass LibraryUSB;
        public USBCustom USB;

        public MainForm()
        {
            //CLASSES-OBJECTS
            LibraryUSB = new USBClass();
            USB = new USBCustom();

            //BEHAVIOUR-FUNCTIONALITY
            AES128Encryption.GetOrGenerateKey();

            //EVENTS
            LibraryUSB.RegisterForDeviceChange(true, this.Handle);      //For USBClass Library Events
            USB.SetupUSBForEvents(LibraryUSB, this);                    //For USBClass Library Events

            //FORMS
            InitializeComponent();

            //TASKS
            Task.Factory.StartNew(() => Serial.InitAllPortsAsync(USB));
        }

        protected override void WndProc(ref Message m)
        {
            bool IsHandled = false;

            LibraryUSB.ProcessWindowsMessage(m.Msg, m.WParam, m.LParam, ref IsHandled);

            base.WndProc(ref m);
        }               //For USBClass Library Events

        private void btnDeviceConnections_Click(object sender, EventArgs e)
        {
            DeviceConnections deviceConnections = new DeviceConnections();
            deviceConnections.BringToFront();
            deviceConnections.Show();
        }
    }
}
