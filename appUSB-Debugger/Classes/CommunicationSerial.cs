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
        public USBLocal usb;
        public SerialLocal serial;

        #region EventSetup
        public delegate void SerialMsgReadEventHandler(object source, EventArgs args);
        public event SerialMsgReadEventHandler SerialMsgRead;
        #endregion EventSetup

        public void Init()
        {
            usb = new USBLocal();
            usb.SearchDevices();
            serial = new SerialLocal();
            usb.DevicesCOMPorts(serial.serialPortNameList);
            serial.InitAllSerialPorts();
        }

        public string SerialReadLine(SerialPort serialPort)
        {
            try
            {
                string messageReceived = serialPort.ReadLine();
                if (messageReceived != "") OnSerialMsgRead();      //register msg read event
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

        #region EventInvokers
        protected virtual void OnSerialMsgRead()
        {
            SerialMsgRead?.Invoke(this, EventArgs.Empty);   //if there are subscribers to this event, (event != null) invoke event
        }
        #endregion EventInvokers
    }
}
