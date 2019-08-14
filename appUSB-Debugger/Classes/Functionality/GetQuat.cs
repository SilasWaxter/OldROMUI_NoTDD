using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appUSB_Debugger
{
    static class GetQuat
    {
        //public static void UpdateQuatLabel(int sensorSpecifier, MainForm mainForm)
        //{
        //    switch (sensorSpecifier)
        //    {
        //        case 1:
        //            string s1q = $"({Sensor1.q[0]}, {Sensor1.q[1]}, {Sensor1.q[2]}, {Sensor1.q[3]})";
        //            if (s1q != "" && s1q != "(0, 0, 0, 0)")
        //                mainForm.lblS1Quat.Text = s1q;
        //            break;
        //        case 2:
        //            string s2q = $"({Sensor2.q[0]}, {Sensor2.q[1]}, {Sensor2.q[2]}, {Sensor2.q[3]})";
        //            if (s2q != "" && s2q != "(0, 0, 0, 0)")
        //                mainForm.lblS1Quat.Text = s2q;
        //            break;
        //    }
        //}

        //public delegate void EventHandler();
        //public static event EventHandler updateQuatS1;
        //public static event EventHandler updateQuatS2;

        //Called whenever Hub.device.serialPort.dataReceived event is triggered.

        public static void CheckIncomingMsgForQuat(string message, MainForm mainForm)
        {
            if (message.Contains("S1 "))
            {
                message = message.Substring(message.IndexOf(' ') + 1);  //sets quatMsg = everything after the ' '
                string[] quat = message.Split(':');                     //splits strings by ':' and stores them in string[]

                //System.Diagnostics.Debug.Write("S1: ");
                for (int i = 0; i < 4; i++)
                {
                    Sensor1.q[i] = Convert.ToDouble(quat[i]);
                    //System.Diagnostics.Debug.Write($"{Sensor1.q[i]}, ");
                }
                //System.Diagnostics.Debug.WriteLine("");

                string s1q = $"({Sensor1.q[0]}, {Sensor1.q[1]}, {Sensor1.q[2]}, {Sensor1.q[3]})";
                if (s1q != "" && s1q != "(0, 0, 0, 0)")
                {
                    mainForm.lblS1Quat.Invoke(new Action(() => mainForm.lblS1Quat.Text = s1q));
                }
            }

            if (message.Contains("S2 "))
            {
                message = message.Substring(message.IndexOf(' ') + 1);  //sets quatMsg = everything after the ' '
                string[] quat = message.Split(':');                     //splits strings by ':' and stores them in string[]

                //System.Diagnostics.Debug.Write("S2: ");
                for (int i = 0; i < 4; i++)
                {
                    Sensor2.q[i] = Convert.ToDouble(quat[i]);
                    //System.Diagnostics.Debug.Write($"{Sensor2.q[i]}, ");
                }
                //System.Diagnostics.Debug.WriteLine("");
            }
            string s2q = $"({Sensor2.q[0]}, {Sensor2.q[1]}, {Sensor2.q[2]}, {Sensor2.q[3]})";
            if (s2q != "" && s2q != "(0, 0, 0, 0)")
            {
                mainForm.lblS2Quat.Invoke(new Action(() => mainForm.lblS2Quat.Text = s2q));
            }
        }
    }
}
