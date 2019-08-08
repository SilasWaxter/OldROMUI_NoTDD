using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using USBClassLibrary;

namespace appUSB_Debugger
{
    public class Device
    {
        public byte[] macAdd = { 0, 0, 0, 0, 0, 0 };     //Mac-Address of device
        public SerialPort serialPort;
        public string comPort;
        public bool serialPortReconnecting = false;

        public List<string> readMsg = new List<string>();

        public void ClearData()
        {
            if(serialPort != null)
            {
                serialPort.Close();
                serialPort.Dispose();
            }


            serialPort = null;
            comPort = null;
            byte[] nullMacAdd = { 0, 0, 0, 0, 0, 0 };
            nullMacAdd = macAdd;
        }
    }
}
