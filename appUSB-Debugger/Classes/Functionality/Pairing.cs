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
    static class Pairing
    {
        public static CancellationTokenSource pairCT = new CancellationTokenSource();
        public static Task PairDeviceTask;

        //Creates Task to classify device and sends "Init. Serial Com." message to device
        public static void Pair(SerialPort port)
        {
            //Creates CancellationToken
            CancellationToken ct = pairCT.Token;
            
            PairDeviceTask = Task.Factory.StartNew(() => ClassifyDevice(port, ct));
        }

        public static void ClassifyDevice(SerialPort port, CancellationToken cancellationToken)
        {
            //while the task's cancellation is NOT requested
            while (!cancellationToken.IsCancellationRequested)
            {
                //Signal "Init. Serial Com." and get response.
                Serial.WriteLine(port, "~ISC~");
                string msgRead = Serial.ReadLine(port);
                //System.Diagnostics.Debug.WriteLine("msgRead from " + port.PortName + ": " + msgRead);

                #region Expected Responses from Serial Init:
                //      HUB:     ("~HUB;MacADD: %s~\n", WiFi.macAddress().c_str())
                //      SENSOR:  ("~SENSOR;MacADD: %s~\n", WiFi.macAddress().c_str())
                #endregion Expected Responses from Serial Init:

                //Checks whether msg is readable and whether the msg's port has already been "claimed" by a device
                if ((MsgFormatCorrect(msgRead)) && (!Msg_sPortAlreadyClassified(port)))
                {
                    msgRead = msgRead.Trim('~');                                        //Remove "~" from msgRead
                    string MacAdd = msgRead.Substring(msgRead.IndexOf(' ') + 1);        //Finds ' ' and returns string from everything after it.

                    if (msgRead.Contains("HUB"))
                    {
                        MacConverter(MacAdd, Hub.device.MacAdd);   //Store Mac in Hub
                        Hub.device.serialPort = port;              //Store Hub's port in Hub


                        System.Diagnostics.Debug.WriteLine("Hub (" + Hub.device.serialPort.PortName + ") Port Mapped");
                        //foreach (UInt16 num in Hub.MacAdd)
                        //    System.Diagnostics.Debug.Write(num);
                        //System.Diagnostics.Debug.WriteLine("");

                        break;
                    }

                    if (msgRead.Contains("SENSOR"))
                    {
                        //pair sensor1
                        if (Sensor1.device.serialPort != port)
                        {
                            MacConverter(MacAdd, Sensor1.device.MacAdd);   //Store Mac in Sensor1
                            Sensor1.device.serialPort = port;              //Store Sensor1's port in Sensor1

                            System.Diagnostics.Debug.WriteLine("Sensor 1 (" + Sensor1.device.serialPort.PortName + ") Port Mapped");
                            //foreach (UInt16 num in Sensor1.MacAdd)
                            //    System.Diagnostics.Debug.Write(num);
                            //System.Diagnostics.Debug.WriteLine("");

                            break;
                        }
                        //pair sensor2
                        if (Sensor1.device.serialPort == port)
                        {
                            MacConverter(MacAdd, Sensor2.device.MacAdd);   //Store Mac in Sensor2
                            Sensor2.device.serialPort = port;              //Store Sensor2's port in Sensor2

                            System.Diagnostics.Debug.WriteLine("Sensor 2 (" + Sensor2.device.serialPort.PortName + ") Port Mapped");
                            //foreach (UInt16 num in Sensor2.MacAdd)
                            //    System.Diagnostics.Debug.Write(num);
                            //System.Diagnostics.Debug.WriteLine("");

                            break;
                        }
                    }
                }
            }
        }

        #region MessageFilters
        //Returns true when Port is already mapped to Device
        private static bool Msg_sPortAlreadyClassified(SerialPort port)
        {
            if ((port == Hub.device.serialPort) || (port == Sensor1.device.serialPort) || (port == Sensor2.device.serialPort))
            {
                return true;
            }
            else return false;
        }
        
        //Returns true if incoming msg is "readable"
        private static bool MsgFormatCorrect(string msg)
        {
            if ((msg != null) && (msg != "") && (msg.StartsWith("~")) && (msg.EndsWith("~")))
            {
                return true;
            }
            else return false;
        }
        #endregion

        //Converts MacAdd String to uint16[] Mac
        private static void MacConverter(string SourceMacAdd, UInt16[] TargetMacAdd)
        {
            string[] MacArray = SourceMacAdd.Split(':');        //split the Mac by its components

            for (int i = 0; i < 6; i++) TargetMacAdd[i] = Convert.ToUInt16(MacArray[i], 16);
        }
    }
}
