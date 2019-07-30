using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;

namespace appUSB_Debugger
{
    public class SerialLocal
    {

        public List<string> serialPortNameList = new List<string>();
        public List<SerialPort> serialPortList = new List<SerialPort>();

        public void InitAllSerialPorts()
        {
            //creates a generic port with correct communication properties
            SerialPort port = new SerialPort();
            port.BaudRate = 115200;
            port.DataBits = 8;
            port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1", true);
            port.Parity = (Parity)Enum.Parse(typeof(Parity), "none", true);
            port.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "XonXoff", true);

            port.ReadTimeout = 150;
            port.WriteTimeout = 1000;

            System.Diagnostics.Debug.WriteLine("Serial Ports Init:  ");

            ///<summary>
            ///for each port named:
            ///     change the name of the generic port to the named port;
            ///     add the named port to the serial_port list;
            ///     add an received message variable for the port;
            ///</summary>
            for (int i = 0; i < serialPortNameList.Count; i++)
            {
                port.PortName = serialPortNameList[i];
                serialPortList.Add(port);           //POSSIBLE PROBLEM:  If function is ran twice, ports will be added multiple times.
                System.Diagnostics.Debug.WriteLine(serialPortList[i].PortName);
                serialPortList[i].Open();
            }
        }
    }
}
