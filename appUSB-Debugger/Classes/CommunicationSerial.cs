using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using USBClassLibrary;

namespace appUSB_Debugger
{ 
    public class CommunicationSerial
    {
        public USBLocal USB = new USBLocal();
        public SerialLocal Serial = new SerialLocal();

        public void Init()
        {
            USB.SearchDevices();
            USB.DevicesCOMPorts(Serial.serialPortNameList);
            Serial.InitAllSerialPorts();
        }

        public string SerialReadLine(SerialPort serialPort)
        {
            try
            {
                string messageReceived = serialPort.ReadLine();
                return messageReceived;
            }
            catch (TimeoutException)
            {
                System.Diagnostics.Debug.WriteLine("Serial read TIMEOUT port: " + serialPort.PortName);
                return null;
            }
        }

        public void SerialWriteLine(SerialPort serialPort, string message)
        {
            try
            {
                serialPort.WriteLine(message);
            }
            catch (TimeoutException)
            {
                System.Diagnostics.Debug.WriteLine("Serial write TIMEOUT port: " + serialPort.PortName);
            }
        }
    }
}
