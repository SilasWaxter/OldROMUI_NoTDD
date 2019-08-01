using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using USBClassLibrary;

namespace appUSB_Debugger
{
    class Pairing
    {
        public void ClassifyDevice(SerialPort port, HubClass Hub, SensorClass Sensor1, SensorClass Sensor2, CommunicationSerial SerialCom)
        {
            while(true)
            {
                string msgRead = SerialCom.SerialReadLine(port);                       //ReadLine from port.

                #region Expected Responses from Serial Init:
                //      HUB:     ("~HUB;MacADD: %s~\n", WiFi.macAddress().c_str())
                //      SENSOR:  ("~SENSOR;MacADD: %s~\n", WiFi.macAddress().c_str())
                #endregion Expected Responses from Serial Init:

                //Checks whether msg is readable and whether the msg's port has already been "claimed" by a device
                if ((MsgFormatCorrect(msgRead)) && (!Msg_sPortAlreadyClassified(port, Hub, Sensor1, Sensor2)))      
                {
                    msgRead = msgRead.Trim('~');                                        //Remove "~" from msgRead
                    string MacAdd = msgRead.Substring(msgRead.IndexOf(' ') + 1);        //Finds ' ' and returns string from everything after it.

                    if (msgRead.Contains("HUB"))
                    {
                        MacConverter(MacAdd, Hub.MacAdd);   //Store Mac in Hub
                        Hub.serialPort = port;              //Store Hub's port in Hub

                        System.Diagnostics.Debug.WriteLine("Hub's Port Mapped");
                        foreach (UInt16 num in Hub.MacAdd) System.Diagnostics.Debug.Write(num);
                        System.Diagnostics.Debug.WriteLine("");
                        System.Diagnostics.Debug.WriteLine(Hub.serialPort.PortName);

                        break;
                    }

                    if (msgRead.Contains("SENSOR"))
                    {
                        //pair sensor1
                        if (Sensor1.serialPort != port)
                        {
                            MacConverter(MacAdd, Sensor1.MacAdd);   //Store Mac in Sensor1
                            Sensor1.serialPort = port;              //Store Sensor1's port in Sensor1

                            System.Diagnostics.Debug.WriteLine("Sensor 1's Port Mapped");
                            foreach (UInt16 num in Sensor1.MacAdd) System.Diagnostics.Debug.Write(num);
                            System.Diagnostics.Debug.WriteLine("");
                            System.Diagnostics.Debug.WriteLine(Sensor1.serialPort.PortName);

                            break;
                        }
                        //pair sensor2
                        if (Sensor1.serialPort == port)
                        {
                            MacConverter(MacAdd, Sensor2.MacAdd);   //Store Mac in Sensor2
                            Sensor2.serialPort = port;              //Store Sensor2's port in Sensor2

                            System.Diagnostics.Debug.WriteLine("Sensor 2's Port Mapped");
                            foreach (UInt16 num in Sensor2.MacAdd) System.Diagnostics.Debug.Write(num);
                            System.Diagnostics.Debug.WriteLine("");
                            System.Diagnostics.Debug.WriteLine(Sensor2.serialPort.PortName);

                            break;
                        }
                    }
                }
            }
            
        }

        #region ClassifyDevicesStuff
        //Returns true when Port is already mapped to Device
        //      -Acts as Extra Layer of Protection bc tasks should end with break
        private bool Msg_sPortAlreadyClassified(SerialPort port, HubClass Hub, SensorClass Sensor1, SensorClass Sensor2)
        {
            if ((port == Hub.serialPort) || (port == Sensor1.serialPort) || (port == Sensor2.serialPort))
            {
                return true;
            }
            else return false;
        }
        //Returns true if msg is "readable"
        private bool MsgFormatCorrect(string msg)
        {
            if ((msg != null) && (msg != "") && (msg.StartsWith("~")) && (msg.EndsWith("~")))
            {
                return true;
            }
            else return false;
        }
        //Converts MacAdd String to uint16[] Mac
        private void MacConverter(string SourceMacAdd, UInt16[] TargetMacAdd)
        {
            string[] MacArray = SourceMacAdd.Split(':');        //split the Mac by its components

            for (int i = 0; i < 6; i++) TargetMacAdd[i] = Convert.ToUInt16(MacArray[i], 16);
        }
        #endregion ClassifyDevicesStuff
    }
}
