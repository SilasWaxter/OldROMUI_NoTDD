using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.IO.Ports;
using USBClassLibrary;


namespace appUSB_Debugger
{
    public static class Serial
    {
        public static List<SerialDevice> devices = new List<SerialDevice>();
        public static SerialDevice deviceBeingAdded;

        public static void InitAllPorts(USBCustom USB)
        {
            //Allows method to be ran multiple times.
            devices.Clear();         

            //create list with current ComPorts
            var comPortSearchResults = new List<string>();
            USB.SearchComPorts(ref comPortSearchResults);

            //Add/Pair devices from comPortSearchResults
            System.Diagnostics.Debug.WriteLine("Serial Ports Init:  ");
            foreach(string comPort in comPortSearchResults)
            {
                AddDevice(comPort);
                Pairing.Pair(deviceBeingAdded.serialPort);
            }
            System.Diagnostics.Debug.WriteLine("");
        }

        public static void AddDevice(string comPort)
        {
            SerialPort port = new SerialPort();
            port.BaudRate = 115200;
            port.DataBits = 8;
            port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1", true);
            port.Parity = (Parity)Enum.Parse(typeof(Parity), "none", true);
            port.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "XonXoff", true);

            port.ReadTimeout = 500;
            port.WriteTimeout = 500;

            port.PortName = comPort;
            port.Open();

            //create portContainer (houses SerialPort and port's name) and add it to public list.
            deviceBeingAdded = new SerialDevice(port, comPort);
            devices.Add(deviceBeingAdded);

            System.Diagnostics.Debug.WriteLine(devices[devices.IndexOf(deviceBeingAdded)].comPort + " OPEN");
        }

        public static void RemoveDevice(string comPort)
        {
            //Go through Device list and compare its comPort to comPort(param).  If ==, remove device from devices List & DEVICE_CLASSES record.
            foreach(SerialDevice device in devices.ToList())
            {
                if (device.comPort == comPort)
                {
                    if (device.serialPort == Hub.device.serialPort)
                        Hub.device.serialPort = null;
                    if (device.serialPort == Sensor1.device.serialPort)
                        Sensor1.device.serialPort = null;
                    if (device.serialPort == Sensor2.device.serialPort)
                        Sensor2.device.serialPort = null;
                    
                    devices.Remove(device);
                }
            }
        }

        public static string ReadLine(SerialPort serialPort)
        {
            try
            {
                string messageReceived = serialPort.ReadLine();
                return messageReceived;
            }
            catch (TimeoutException)
            {
                System.Diagnostics.Debug.WriteLine("Serial read TIMEOUT port: " + serialPort.PortName);
                //Close Port
                serialPort.Close();
                serialPort.Dispose();

                //Remove and Add/Pair Device
                RemoveDevice(serialPort.PortName);

                if(Pairing.PairDeviceTask.Status == TaskStatus.Running)
                {
                    //Cancel task and dispose of TokenSource.
                    Pairing.pairCT.Cancel();
                    Pairing.pairCT.Dispose();

                    //create a new cancelationTokenSource.
                    Pairing.pairCT = new CancellationTokenSource();
                }

                AddDevice(serialPort.PortName);
                Pairing.Pair(deviceBeingAdded.serialPort);
                return null;
            }
        }

        public static void WriteLine(SerialPort serialPort, string message)
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

        public static List<string> CurrentComPortsInContainers()
        {
            var currentCom = new List<string>();
            foreach(SerialDevice device in devices)
            {
                currentCom.Add(device.comPort);
            }
            return currentCom;
        }
    }

    //Type that contains serialPort and comPort for eachDevice
    public class SerialDevice
    {
        public SerialPort serialPort { get; set; }
        public string comPort { get; set; }

        public SerialDevice(SerialPort setPort, string setPortName)
        {
            this.serialPort = setPort;
            this.comPort = setPortName;
        }
    }
}
