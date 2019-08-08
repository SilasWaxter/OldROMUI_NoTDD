using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

using System.IO;

using System.IO.Ports;


namespace appUSB_Debugger
{
    public static class Serial
    {

        public static List<Device> ignoredDevices = new List<Device>();
        public static async void InitAllPortsAsync(USBCustom USB)
        {
            //create list with current ComPorts
            var comPortSearchResults = new List<string>();
            USB.SearchComPorts(ref comPortSearchResults);

            //Add/Pair devices from comPortSearchResults
            for(int i = 0; i < comPortSearchResults.Count; i++) 
            {
                bool addDeviceSuccess = await AddDevice(comPortSearchResults[i]);
                if (!addDeviceSuccess)
                    i = i - 1;
            }
        }

        //Adds device from its comPort name.  Contains SerialPort.DataReceived event (invoked when data is received).  Data stored in device(object).readMsg(List<string>)
        public static Task<bool> AddDevice(string comPort)
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                //Set serialPort attributes.
                SerialPort serialPort = new SerialPort();
                serialPort.BaudRate = 115200;
                serialPort.DataBits = 8;
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1", true);
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "none", true);
                serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "XonXoff", true);

                serialPort.PortName = comPort;
                serialPort.ReadTimeout = 2000;
                serialPort.WriteTimeout = 500;

                //Open the port.
                serialPort.Open();

                //Create a Device object to contain the SerialPort, the SerialPort.PortName, the List<string> readMsg (holds received msgs), and the MacAdd.
                Device currentDevice = new Device();
                //Save serialPort and comPort in currentDevice
                currentDevice.serialPort = serialPort;
                currentDevice.comPort = comPort;

                //serialPort event that's invoked when data is received.  Only adds msg to the device's class's List<string> readMsg, if the msg starts and ends with "~"
                serialPort.DataReceived += (object sender, SerialDataReceivedEventArgs eventArgs) =>
                {
                    string message = "";
                    try
                    {
                        message = serialPort.ReadLine();
                    }
                    catch
                    {
                        if(!currentDevice.serialPortReconnecting)
                            ReconnectToDeviceAsync(currentDevice);
                        return;
                    }
                    //Only adds messages with "~msg~" format.
                    if (message.StartsWith("~") && message.EndsWith("~"))
                    {
                        currentDevice.readMsg.Add(message);

                        //OUTPUT INCOMING MESSAGES
                        //System.Diagnostics.Debug.WriteLine(message);
                    }
                };


                ClassifyDevice(currentDevice);
                return true;
            });
        }

        #region DeviceClassification
        private static void ClassifyDevice(Device currentDevice)
        {
            #region Expected Responses from Serial Init:
            //      HUB:     ("~HUB;MacADD: %s~\n", WiFi.macAddress().c_str())
            //      SENSOR:  ("~SENSOR;MacADD: %s~\n", WiFi.macAddress().c_str())
            #endregion Expected Responses from Serial Init:

            string iscResponse;
            iscResponse = WaitForISCResponse(currentDevice);
            if (iscResponse == null)
                return;

            //Gather and Convert Received Mac Address.
            iscResponse = iscResponse.Trim('~');                                                //Trims '~' off msg.
            string macAddReceived = iscResponse.Substring(iscResponse.IndexOf(' ') + 1);        //Finds ' ' and returns unformated, received macAdd.
            string[] macAddSplit = macAddReceived.Split(':');                                   //split the Mac into its bytes.
            byte[] macAdd = new byte[6];                                                        //Byte[] storage for macAdd.
            for (int i = 0; i < 6; i++)                                                         //Converts string[] macAddSplit (HEX/base16) to byte[]
                macAdd[i] = Convert.ToByte(macAddSplit[i], 16);

            //Store the MacAdd in Device obj.
            currentDevice.macAdd = macAdd;
            AddDetailsToStaticClass(currentDevice, iscResponse, macAddReceived);
        }

        private static string WaitForISCResponse(Device currentDevice)
        {
            //Ensure that there are no msgs in the readMsg list.
            currentDevice.readMsg.Clear();

            //"Init. Serial Com."
            Serial.WriteLine(currentDevice.serialPort, "~ISC~");

            //Delay
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < 250) ;

            //Wait for a msg.
            if (currentDevice.readMsg.Count == 0)
            {
                ReconnectToDeviceAsync(currentDevice);
                return null;
            }

            //Check if any msgs in readMsg List<string> contain part of the expected Responses from Serial Init.
            for (int i = 0; i < currentDevice.readMsg.Count; i++)
                if (currentDevice.readMsg[i].Contains("HUB;MacADD:") || currentDevice.readMsg[i].Contains("SENSOR;MacADD:"))
                    return currentDevice.readMsg[i];

            return null;
        }

        private static void AddDetailsToStaticClass(Device currentDevice, string iscResponse, string macAddReceived)
        {
            #region networkDetails.txt Format:
            //      1st Line:   Aes128Key[key]
            //                  Sensor1;macAdd[1:2:3:4:5:6]
            //                  Sensor2;macAdd[1:2:3:4:5:6]
            //                  Hub;macAdd[1:2:3:4:5:6]
            #endregion
            string filePath = @"C:\Users\silas\Desktop\appUSB\appUSB-Debugger\networkDetails.txt";

            List<string> linesFromNetDetails = File.ReadAllLines(filePath).ToList();

            if (iscResponse.Contains("HUB"))
            {
                bool hubStored = false;

                //Check if currentDevice is stored in networkDetails.txt
                foreach (string line in linesFromNetDetails)
                {
                    //Check if current line contains "Hub" and the macAdd of the currentDevice.
                    if (line.Contains("Hub") && line.Contains(macAddReceived))
                    {
                        //currentDevice is Hub's recorded device
                        Hub.device = currentDevice;
                        System.Diagnostics.Debug.WriteLine($"Hub added from records on ({Hub.device.comPort})");
                        return;
                    }

                    //record wether there is a hub stored
                    if (line.Contains("Hub"))
                        hubStored = true;
                }

                //If there is no hub stored in networkDetails.txt, Hub.device = currentDevice and write/record Hub/currentDevice in networkDetails.txt
                if (!hubStored)
                {
                    Hub.device = currentDevice;
                    System.Diagnostics.Debug.WriteLine($"No records found.  Adding Hub on ({Hub.device.comPort})");
                    linesFromNetDetails.Add($"Hub;macAdd[{macAddReceived}]");
                    File.WriteAllLines(filePath, linesFromNetDetails);
                    return;
                }
                //A Hub is already stored, and it is not this Hub.
                using (NewHubFound newHubFound = new NewHubFound())                         //Displays NewHubFound form as a dialog.
                {
                    if (newHubFound.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //Checks if dialog set its returnText to "replace"
                        if (newHubFound.returnText == "replace")
                        {
                            foreach (string line in linesFromNetDetails)
                            {
                                if (line.Contains("Hub"))
                                {
                                    linesFromNetDetails.Remove(line);
                                    Hub.device = currentDevice;
                                    System.Diagnostics.Debug.WriteLine($"Replacing Hub in Records.  Adding Hub on ({Hub.device.comPort})");
                                    linesFromNetDetails.Add($"Hub;macAdd[{macAddReceived}]");
                                    File.WriteAllLines(filePath, linesFromNetDetails);
                                    return;
                                }
                            }
                        }
                    }
                    //If dialog is canceled or ignored,     (if replace command is NOT selected and OK is clicked, execution reaches this point.)
                    System.Diagnostics.Debug.WriteLine($"Ignoring Hub on ({currentDevice.comPort})");
                    ignoredDevices.Add(currentDevice);
                    return;
                }
            }

            if (iscResponse.Contains("SENSOR"))
            {
                bool sensor1Stored = false, sensor2Stored = false;

                //Check if currentDevice is stored in networkDetails.txt
                foreach (string line in linesFromNetDetails)
                {
                    //Check if current line contains "Sensor1" and the macAdd of the currentDevice.
                    if (line.Contains("Sensor1") && line.Contains(macAddReceived))
                    {
                        //currentDevice is Sensor1's recorded device
                        Sensor1.device = currentDevice;
                        System.Diagnostics.Debug.WriteLine($"Sensor 1 added from records on ({Sensor1.device.comPort})");
                        return;
                    }

                    //Check if line contains "Sensor2" and the macAdd of the currentDevice.
                    if (line.Contains("Sensor2") && line.Contains(macAddReceived))
                    {
                        //currentDevice is Sensor2's recorded device
                        Sensor2.device = currentDevice;
                        System.Diagnostics.Debug.WriteLine($"Sensor 2 added from records on ({Sensor2.device.comPort})");
                        return;
                    }

                    //Record which sensors are stored.
                    if (line.Contains("Sensor1"))
                        sensor1Stored = true;
                    if (line.Contains("Sensor2"))
                        sensor2Stored = true;
                }

                if (!sensor1Stored)
                {
                    Sensor1.device = currentDevice;
                    System.Diagnostics.Debug.WriteLine($"No records found.  Adding Sensor 1 on ({Sensor1.device.comPort})");
                    linesFromNetDetails.Add($"Sensor1;macAdd[{macAddReceived}]");
                    File.WriteAllLines(filePath, linesFromNetDetails);
                    return;
                }
                if (!sensor2Stored)
                {
                    Sensor2.device = currentDevice;
                    System.Diagnostics.Debug.WriteLine($"No record for this device found.  Adding Sensor 2 on ({Sensor2.device.comPort})");
                    linesFromNetDetails.Add($"Sensor2;macAdd[{macAddReceived}]");
                    File.WriteAllLines(filePath, linesFromNetDetails);
                    return;
                }

                //Both Sensors are already stored, and neither of the stored sensors are the currentDevice

                using (NewSensorFound newSensorFound = new NewSensorFound())                         //Displays NewHubFound form as a dialog.
                {
                    if (newSensorFound.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //Checks if dialog set its returnText to "replaceSensor1"
                        if (newSensorFound.returnText == "replaceSensor1")
                        {
                            foreach (string line in linesFromNetDetails)
                            {
                                if (line.Contains("Sensor1"))
                                {
                                    linesFromNetDetails.Remove(line);
                                    Sensor1.device = currentDevice;
                                    System.Diagnostics.Debug.WriteLine($"Replacing Sensor 1 in Records.  Adding Sensor 1 on ({Sensor1.device.comPort})");
                                    linesFromNetDetails.Add($"Sensor1;macAdd[{macAddReceived}]");
                                    File.WriteAllLines(filePath, linesFromNetDetails);
                                    newSensorFound.Close();
                                    return;
                                }
                            }
                        }
                        //Checks if dialog set its returnText to "replaceSensor2"
                        if (newSensorFound.returnText == "replaceSensor2")
                        {
                            foreach (string line in linesFromNetDetails)
                            {
                                if (line.Contains("Sensor2"))
                                {
                                    linesFromNetDetails.Remove(line);
                                    Sensor2.device = currentDevice;
                                    System.Diagnostics.Debug.WriteLine($"Replacing Sensor 2 in Records.  Adding Sensor 2 on ({Sensor2.device.comPort})");
                                    linesFromNetDetails.Add($"Sensor2;macAdd[{macAddReceived}]");
                                    File.WriteAllLines(filePath, linesFromNetDetails);
                                    return;
                                }

                            }
                        }
                    }

                    //If dialog is canceled or ignored,     (if neither replace commands are selected and OK is clicked, execution reaches this point.)
                    System.Diagnostics.Debug.WriteLine($"Ignoring Sensor on ({currentDevice.comPort})");
                    ignoredDevices.Add(currentDevice);
                    return;
                }
            }
        }
        #endregion

        public static void RemoveDevice(string comPort)
        {
            if(Hub.device != null)
                if (Hub.device.comPort == comPort)
                    Hub.device.ClearData();

            if (Sensor1.device != null)
                if (Sensor1.device.comPort == comPort)
                    Sensor1.device.ClearData();

            if (Sensor2.device != null)
                if (Sensor2.device.comPort == comPort)
                    Sensor2.device.ClearData();

            if (ignoredDevices.Count > 0)
                for (int i = 0; i < ignoredDevices.Count; i++)
                    if (ignoredDevices[i].comPort == comPort)
                        ignoredDevices.Remove(ignoredDevices[i]);
        }

        public static async void ReconnectToDeviceAsync(Device currentDevice)
        {
            currentDevice.serialPortReconnecting = true;

            System.Diagnostics.Debug.WriteLine($"reconnecting to {currentDevice.comPort}...");

            //delay
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds <= 1000) ;
            sw.Stop();

            //Close Port
            currentDevice.serialPort.Close();

            //Remove and Add Device
            RemoveDevice(currentDevice.comPort);
            await AddDevice(currentDevice.comPort);

            currentDevice.serialPortReconnecting = false;
        }

        public static bool ReceivedMessageBool(SerialPort serialPort, string messageReceived)
        {
            //Find device being referenced.
            Device device = DeviceFromSerialPort(serialPort);

            //if message is found to be == messageReceived, delete the msg from device.readMsg and return true
            if (device.readMsg.Contains(messageReceived))
            {
                device.readMsg.Remove(messageReceived);
                return true;
            }
            //if no messages are found to be == messageReceived, return False
            return false;
        }

        public static void WriteLine(SerialPort serialPort, string message)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.WriteLine(message);
                }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Serial write EXCEPTION port: " + serialPort.PortName);
                ReconnectToDeviceAsync(DeviceFromSerialPort(serialPort));
            }
        }

        public static void TransferBytes(SerialPort serialPort, byte[] arrayByte)
        {
            //clears read msg storage.
            //DeviceFromSerialPort(serialPort).readMsg.Clear();
            for (int i = 0; i < arrayByte.Length; i++)
            {
                //Write Byte
                Serial.WriteLine(serialPort, $"~{arrayByte[i].ToString()}~");                        

                //while Byte is not read,
                while (!Serial.ReceivedMessageBool(serialPort, $"~{arrayByte[i].ToString()}~"))
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    if (sw.ElapsedMilliseconds >= 1000)         //after 1000 milliseconds, try byte again.
                    {
                        Serial.WriteLine(serialPort, "~IncByteRetry~");
                        i = i - 1;
                        System.Diagnostics.Debug.WriteLine("byte transfer NOT successful");
                        break;
                    }
                }
            }
        }

        public static List<string> ComPortsClassified()
        {
            var currentCom = new List<string>();
            
            //Add classified devices to curentCom list
            if (Hub.device != null)
                currentCom.Add(Hub.device.comPort);

            if (Sensor1.device != null)
                currentCom.Add(Sensor1.device.comPort);

            if (Sensor2.device != null)
                currentCom.Add(Sensor2.device.comPort);

            if(ignoredDevices.Count > 0)
                foreach(Device device in ignoredDevices)
                    currentCom.Add(device.comPort);

            return currentCom;
        }

        public static Device DeviceFromSerialPort(SerialPort serialPort)
        {
            if (serialPort == Hub.device.serialPort)
                return Hub.device;

            if (serialPort == Sensor1.device.serialPort)
                return Sensor1.device;

            if (serialPort == Sensor2.device.serialPort)
                return Sensor2.device;

            //If serialPort is not contained.  Return null.  Will likely cause exception indicating issue.
            return null;
        }
    }
}
