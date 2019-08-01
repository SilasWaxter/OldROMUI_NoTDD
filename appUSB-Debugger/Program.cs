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
            //CLASSES-OBJECTS
            HubClass Hub = new HubClass();
            SensorClass Sensor1 = new SensorClass();
            SensorClass Sensor2 = new SensorClass();

            AES128Encryption aes128 = new AES128Encryption();
            CommunicationSerial SerialCom = new CommunicationSerial();
            Pairing pairing = new Pairing();

            //BEHAVIOUR-FUNCTIONALITY
            aes128.GenerateKey();
            SerialCom.Init();

            //Init. Serial Communication and Map Ports to Devices
            foreach (SerialPort port in SerialCom.Serial.serialPortList)
            {
                Task MapPortToDevice = Task.Factory.StartNew(() => pairing.ClassifyDevice(port, Hub, Sensor1, Sensor2, SerialCom));
            }

            foreach (SerialPort port in SerialCom.Serial.serialPortList) SerialCom.SerialWriteLine(port, "~ISC~");

            //EVENTS

            //TASKS

            //FORMS

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form1 = new Form1();
            Application.Run(form1);
        }
    }
}