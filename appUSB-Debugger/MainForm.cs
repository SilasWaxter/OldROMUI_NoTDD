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
            System.Diagnostics.Debug.WriteLine("");


            

            //EVENTS
            LibraryUSB.RegisterForDeviceChange(true, this.Handle);      //For USBClass Library Events

            //TASKS

            //FORMS
            InitializeComponent();

            USB.SetupUSBForEvents(LibraryUSB, this);

            Task setupSerialPorts = Task.Factory.StartNew(async () =>
            {
                Serial.InitAllPortsAsync(USB);
            });
        }

        protected override void WndProc(ref Message m)
        {
            bool IsHandled = false;

            LibraryUSB.ProcessWindowsMessage(m.Msg, m.WParam, m.LParam, ref IsHandled);

            base.WndProc(ref m);
        }               //For USBClass Library Events

        private async void Pair_ClickAsync(object sender, EventArgs e)
        {
            //Create a task that completes when/if a Sensor1 device is plugged in
            Task s1PluggedIn = Task.Factory.StartNew(() => Pairing.WaitForDeviceClassification(Sensor1.device));
            await s1PluggedIn;

            //Start Sensor1 pair.
            Pairing.Pair(Sensor1.device.serialPort, this);

            //Create a task that completes when/if a Sensor2 device is plugged in
            Task s2PluggedIn = Task.Factory.StartNew(() => Pairing.WaitForDeviceClassification(Sensor2.device));
            await s2PluggedIn;

            //Start Sensor2 pair.
            Pairing.Pair(Sensor2.device.serialPort, this);

            //Create a task that completes when/if a Sensor1 device is plugged in
            Task hubPluggedIn = Task.Factory.StartNew(() => Pairing.WaitForDeviceClassification(Hub.device));
            await hubPluggedIn;
            //Hub Pair is last.  Needs both Sensor 1 and Sensor 2 MacAdd.
            Pairing.Pair(Hub.device.serialPort, this);
        }
    }
}
