using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.IO.Ports;
using USBClassLibrary;
using System.Diagnostics;

namespace appUSB_Debugger
{
    static class Pairing
    {
        //Async Pair Method
        public static async void Pair(Device currentDevice)
        {
            //Find and store device [Device.cs object and Parent Class's name] from serialPort param.
            string currentDeviceType = null;
            if (currentDevice.comPort == Sensor1.device.comPort)
                currentDeviceType = "Sensor 1";
            if (currentDevice.comPort == Sensor2.device.comPort)
                currentDeviceType = "Sensor 2";
            if (currentDevice.comPort == Hub.device.comPort)
                currentDeviceType = "Hub";


            System.Diagnostics.Debug.WriteLine($"Starting {currentDeviceType} Pair...\n");
            //System.Diagnostics.Debug.Write("\tSharing Key\n");

            //Share Key
            bool shareKeySuccess = await Pairing.SharKeyAsync(currentDevice);

            //Share MacAdd(s)
            #region ShareMacAdds
            if(currentDeviceType == "Sensor 1")
            {
                //System.Diagnostics.Debug.WriteLine($"\tSharing Hub MacAdd to Sensor 1\n");
                bool shareMacAddSuccess = await ShareMacAddAsync(currentDevice, false, false, true);
            }
            if (currentDeviceType == "Sensor 2")
            {
                //System.Diagnostics.Debug.WriteLine($"\tSharing Hub MacAdd to Sensor 2\n");
                bool shareMacAddSuccess = await ShareMacAddAsync(currentDevice, false, false, true);
            }
            if (currentDeviceType == "Hub")
            {
                //Share Sensor1 MacAdd
                //System.Diagnostics.Debug.WriteLine($"\tSharing Sensor1 MacAdd to Hub\n");
                bool shareSensor1MacAddSuccess = await ShareMacAddAsync(currentDevice, true, false, false);

                //Share Sensor2 MacAdd
                //System.Diagnostics.Debug.WriteLine($"\tSharing Sensor2 MacAdd to Hub\n");
                bool shareSensor2MacAddSuccess = await ShareMacAddAsync(currentDevice, false, true, false);
            }
            #endregion ShareMacAdds

            //Restart Device
            Serial.Write(currentDevice, "~restart~");                               //restart device.  set key to key_from_eeprom.  connect to hub via hubMacAdd_from_eeprom.
            while (!Serial.ReceivedMessageBool(currentDevice, "~restarting~")) ;    //wait for restart confirmation.

            System.Diagnostics.Debug.WriteLine($"{currentDeviceType} Pair Complete.");
            if (currentDeviceType == "Sensor 1" || currentDeviceType == "Sensor 2")
            {
                System.Diagnostics.Debug.WriteLine($"\t\tSafe to Remove {currentDeviceType}\n");
            }
        }

        //Task Wrappers
        private static Task<bool> SharKeyAsync(Device currentDevice)
        {
            return Task.Factory.StartNew(() => (ShareKey(currentDevice)));
        }

        private static Task<bool> ShareMacAddAsync(Device currentDevice, bool sensor1 = false, bool sensor2 = false, bool hub = false)
        {
            return Task.Factory.StartNew(() => (ShareMacAdd(currentDevice, sensor1, sensor2, hub)));
        }

        //Actual Functions and Methods
        private static bool ShareKey(Device currentDevice)
        {
            //Prompt Device if its R2Rkey (Ready to Receive Key)
            Serial.Write(currentDevice, "~R2Rkey~");

            //Wait for device to accept prompt,
            while (!Serial.ReceivedMessageBool(currentDevice, "~R2Rkey~")) ;//System.Diagnostics.Debug.Write("Waiting for ~R2Rkey~ Confirmation");

            Serial.TransferBytes(currentDevice, AES128Encryption.key);
            System.Diagnostics.Debug.WriteLine($"Key Transfer to ({currentDevice.serialPort.PortName}) Complete");
            return true;
        }

        private static bool ShareMacAdd(Device currentDevice, bool sensor1 = false, bool sensor2 = false, bool hub = false)
        {
            //Select MacAdd's_Device based on Parameters
            string sourceMacAdd = null;                         //Contains macAdd's device name (ie. sensor1, sensor2, hub)
            byte[] macAddShared = { 0, 0, 0, 0, 0, 0 };         //Mac-Address of device
            if (sensor1)
            {
                sourceMacAdd = "s1";
                macAddShared = Sensor1.device.macAdd;
            }
            if (sensor2)
            {
                sourceMacAdd = "s2";
                macAddShared = Sensor2.device.macAdd;
            }
            if (hub)
            {
                sourceMacAdd = "hub";
                macAddShared = Hub.device.macAdd;
            }

            //if none of the devices were selected, return false indicating share failed
            if (sourceMacAdd == null)
            {
                System.Diagnostics.Debug.WriteLine($"ShareMacAdd ({currentDevice.serialPort.PortName}): no device selected");
                return false;
            }

            //Prompt Device if its R2RMac (Ready to Receive Mac)
            Serial.Write(currentDevice, "~R2RMac~");

            //Wait for device to accept prompt,
            while (!Serial.ReceivedMessageBool(currentDevice, "~R2RMac~")) ; //System.Diagnostics.Debug.Write("Waiting for ~R2RMac~ Confirmation");

            //Tell device which device's mac is being shared.
            Serial.Write(currentDevice, $"~{sourceMacAdd}~");

            //Wait for device confirmation,
            while (!Serial.ReceivedMessageBool(currentDevice, $"~{sourceMacAdd}~")) ; //System.Diagnostics.Debug.Write($"Waiting for ~{sourceMacAdd}~ Confirmation");

            //Share macAdd
            Serial.TransferBytes(currentDevice, macAddShared);
            System.Diagnostics.Debug.WriteLine($"{sourceMacAdd}macAdd Transfer to ({currentDevice.serialPort.PortName}) Complete");

            return true;
        }     
    }
}
