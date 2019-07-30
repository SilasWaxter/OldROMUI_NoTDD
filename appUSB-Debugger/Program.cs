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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //CLASSES-OBJECTS
            AES128Encryption aes128 = new AES128Encryption();
            CommunicationSerial comSerial = new CommunicationSerial();
            Pairing pairing = new Pairing();

            //BEHAVIOUR-FUNCTIONALITY
            aes128.GenerateKey();
            comSerial.Init();

            //EVENTS
            comSerial.SerialMsgRead += pairing.OnSerialMsgRead;

            //TASKS
            Task SerialReader = new Task(() => pairing.MapPorts(comSerial));
            SerialReader.Start();
            
            //FORMS
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    class Pairing
    {
        public void OnSerialMsgRead(object source, EventArgs args)
        {
            //System.Diagnostics.Debug.WriteLine("EVENT: Serial Message Read");
        }

        public void MapPorts(CommunicationSerial comSerialObj)
        {
            while (true)
            {
                string msgWrite = "~ISC~";         //Init. Serial Communication
                comSerialObj.SerialWriteLine(comSerialObj.serial.serialPortList[0], msgWrite);

                string msgRead = comSerialObj.SerialReadLine(comSerialObj.serial.serialPortList[0]);
                if (msgRead != "") System.Diagnostics.Debug.WriteLine(msgRead);
            }
        }
    }
}
