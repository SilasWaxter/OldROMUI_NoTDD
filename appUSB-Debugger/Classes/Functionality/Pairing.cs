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
        //Async Pair Method
        public static async void Pair(SerialPort serialPort, MainForm form)
        {
            //Find and store device [Device.cs object and Parent Class's name] from serialPort param.
            Device currentDevice = Serial.DeviceFromSerialPort(serialPort);
            string currentDeviceType = null;
            if (currentDevice.comPort == Sensor1.device.comPort)
                currentDeviceType = "Sensor 1";
            if (currentDevice.comPort == Sensor2.device.comPort)
                currentDeviceType = "Sensor 2";
            if (currentDevice.comPort == Hub.device.comPort)
                currentDeviceType = "Hub";

            form.tbStatus.AppendText($"Starting {currentDeviceType} Pair...\n");
            form.tbStatus.AppendText("\tSharing Key\n");
            bool shareKeySuccess = await Pairing.SharKeyAsync(currentDevice.serialPort);

            if(currentDeviceType == "Sensor 1")
            {
                form.tbStatus.AppendText($"\tSharing Hub Mac Address to Sensor 1\n");
                bool shareMacAddSuccess = await ShareMacAddAsync(currentDevice.serialPort, false, false, true);
            }
            if (currentDeviceType == "Sensor 2")
            {
                form.tbStatus.AppendText($"\tSharing Hub Mac Address to Sensor 2\n");
                bool shareMacAddSuccess = await ShareMacAddAsync(currentDevice.serialPort, false, false, true);
            }
            if (currentDeviceType == "Hub")
            {
                //Share Sensor1 MacAdd
                form.tbStatus.AppendText($"\tSharing Sensor1 Mac Address to Hub\n");
                bool shareSensor1MacAddSuccess = await ShareMacAddAsync(currentDevice.serialPort, true, false, false);

                //Share Sensor2 MacAdd
                form.tbStatus.AppendText($"\tSharing Sensor2 Mac Address to Hub\n");
                bool shareSensor2MacAddSuccess = await ShareMacAddAsync(currentDevice.serialPort, false, true, false);
            }

            form.tbStatus.AppendText($"{currentDeviceType} Pair Complete.\n");
            if (currentDeviceType == "Sensor 1" || currentDeviceType == "Sensor 2")
                form.tbStatus.AppendText($"Safe to Remove {currentDeviceType}\n");
        }

        //Task Wrappers
        private static Task<bool> SharKeyAsync(SerialPort serialPort)
        {
            return Task.Factory.StartNew(() => (ShareKey(serialPort)));
        }

        private static Task<bool> ShareMacAddAsync(SerialPort serialPort, bool sensor1 = false, bool sensor2 = false, bool hub = false)
        {
            return Task.Factory.StartNew(() => (ShareMacAdd(serialPort, sensor1, sensor2, hub)));
        }

        //Actual Functions and Methods
        private static bool ShareKey(SerialPort serialPort)
        {
            //Get device from serialPort
            Device currentDevice = Serial.DeviceFromSerialPort(serialPort);

            //Prompt Device if its R2Rkey (Ready to Receive Key)
            Serial.WriteLine(serialPort, "~R2Rkey~");

            //Wait for device to accept prompt,
            while (!Serial.ReceivedMessageBool(serialPort, "~R2Rkey~")) ;//System.Diagnostics.Debug.WriteLine("Waiting for ~R2Rkey~ Confirmation");

            Serial.TransferBytes(serialPort, AES128Encryption.key);
            System.Diagnostics.Debug.WriteLine($"Key Transfer to ({serialPort.PortName}) Complete");
            return true;
        }

        private static bool ShareMacAdd(SerialPort serialPort, bool sensor1 = false, bool sensor2 = false, bool hub = false)
        {
            //Get share_target_device from serialPort
            Device currentDevice = Serial.DeviceFromSerialPort(serialPort);

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
                System.Diagnostics.Debug.WriteLine($"ShareMacAdd ({serialPort.PortName}): no device selected");
                return false;
            }

            //Prompt Device if its R2RMac (Ready to Receive Mac)
            Serial.WriteLine(serialPort, "~R2RMac~");

            //Wait for device to accept prompt,
            while (!Serial.ReceivedMessageBool(serialPort, "~R2RMac~")) ; //System.Diagnostics.Debug.WriteLine("Waiting for ~R2RMac~ Confirmation");

            //Tell device which device's mac is being shared.
            Serial.WriteLine(serialPort, $"~{sourceMacAdd}~");

            //Wait for device confirmation,
            while (!Serial.ReceivedMessageBool(serialPort, $"~{sourceMacAdd}~")) ; //System.Diagnostics.Debug.WriteLine($"Waiting for ~{sourceMacAdd}~ Confirmation");
            
            //Share macAdd
            Serial.TransferBytes(serialPort, macAddShared);
            System.Diagnostics.Debug.WriteLine($"{sourceMacAdd}macAdd Transfer to ({serialPort.PortName}) Complete");

            return true;
        }     

        public static void WaitForDeviceClassification(Device device)           //Ensures Device is plugged in.
        {
            while (device.serialPort == null) ;
        }
    }
}
