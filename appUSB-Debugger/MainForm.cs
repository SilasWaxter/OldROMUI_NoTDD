using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

using USBClassLibrary;
using System.IO.Ports;

namespace appUSB_Debugger
{
    public partial class MainForm : Form
    {
        public USBClass LibraryUSB = new USBClass();
        public USBCustom USB = new USBCustom();

        public MainForm()
        {
            //CLASSES-OBJECTS


            //BEHAVIOUR-FUNCTIONALITY
            AES128Encryption.GetOrGenerateKey();

            //EVENTS
            LibraryUSB.RegisterForDeviceChange(true, Handle);      //For USBClass Library Events
            USB.SetupUSBForEvents(LibraryUSB, this);                    //For USBClass Library Events

            //GetQuat.updateQuatS1 += () => GetQuat.UpdateQuatLabel(1, this);
            //GetQuat.updateQuatS2 += () => GetQuat.UpdateQuatLabel(2, this);

            //FORMS
            InitializeComponent();

            //TASKS
            Task.Factory.StartNew(() => Serial.InitAllPortsAsync(USB, this));
           

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

        //private void bgndwrkrUpdateUIQuat_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    string message = (string)e.Argument;                        //sets local string "message" to string passed in as an argument.

        //    if (message.Contains("S1 "))
        //    {
        //        message = message.Substring(message.IndexOf(' ') + 1);  //sets quatMsg = everything after the ' '
        //        string[] quat = message.Split(':');                     //splits strings by ':' and stores them in string[]

        //        //System.Diagnostics.Debug.Write("S1: ");
        //        for (int i = 0; i < 4; i++)
        //        {
        //            Sensor1.q[i] = Convert.ToDouble(quat[i]);
        //            e.Result = "1";

        //            //System.Diagnostics.Debug.Write($"{Sensor1.q[i]}, ");
        //        }
        //        //System.Diagnostics.Debug.WriteLine("");
        //    }
        //    if (message.Contains("S2 "))
        //    {
        //        message = message.Substring(message.IndexOf(' ') + 1);  //sets quatMsg = everything after the ' '
        //        string[] quat = message.Split(':');                     //splits strings by ':' and stores them in string[]

        //        //System.Diagnostics.Debug.Write("S2: ");
        //        for (int i = 0; i < 4; i++)
        //        {
        //            Sensor2.q[i] = Convert.ToDouble(quat[i]);
        //            e.Result = "2";

        //            //System.Diagnostics.Debug.Write($"{Sensor2.q[i]}, ");
        //        }
        //        //System.Diagnostics.Debug.WriteLine("");
        //    }
        //}

        //private void bgndwrkrUpdateUIQuat_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if(e.Error != null)
        //    {
        //        System.Diagnostics.Debug.WriteLine(e.Error.Message);
        //    }
        //    else
        //    {
        //        if(e.Result.ToString() == "1")
        //        {
        //            string s1q = $"({Sensor1.q[0]}, {Sensor1.q[1]}, {Sensor1.q[2]}, {Sensor1.q[3]})";
        //            if (s1q != "" && s1q != "(0, 0, 0, 0)")
        //            {
        //                lblS1Quat.Invoke(new Action(() => lblS1Quat.Text = s1q));
        //            }
        //        }
        //        else if (e.Result.ToString() == "2")
        //        {
        //            string s2q = $"({Sensor2.q[0]}, {Sensor2.q[1]}, {Sensor2.q[2]}, {Sensor2.q[3]})";
        //            if (s2q != "" && s2q != "(0, 0, 0, 0)")
        //            {
        //                lblS1Quat.Invoke(new Action(() => lblS1Quat.Text = s2q));
        //            }
        //        }
        //    }
        //}
    }
}
