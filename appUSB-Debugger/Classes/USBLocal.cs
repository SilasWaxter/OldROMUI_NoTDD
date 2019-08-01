using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using USBClassLibrary;

namespace appUSB_Debugger
{
    public class USBLocal
    {
        public uint VID = 0x1A86, PID = 0x7523;
        private USBClass PortUSB = new USBClass();
        private List<USBClass.DeviceProperties> ListOfUSBDeviceProperties;

        public void SearchDevices()
        {
            
            ListOfUSBDeviceProperties = new List<USBClass.DeviceProperties>();
            PortUSB.USBDeviceAttached += new USBClass.USBDeviceEventHandler(USBDeviceAttached);
            PortUSB.USBDeviceRemoved += new USBClass.USBDeviceEventHandler(USBDeviceRemoved);
            PortUSB.RegisterForDeviceChange(true, form.Handle);
        }

        public void DevicesCOMPorts(List<string> COMPorts)
        {
            if (USBClass.GetUSBDevice(VID, PID, ref ListOfUSBDeviceProperties, true))
            {
                System.Diagnostics.Debug.WriteLine("Found com_ports:  ");

                foreach (var device in ListOfUSBDeviceProperties)
                {
                    COMPorts.Add(device.COMPort);

                    System.Diagnostics.Debug.WriteLine(device.COMPort);
                }
            }
        }

        private void USBDeviceAttached(object sender, USBClass.USBDeviceEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("            USB DEVICE ATTACHED");
        }

        private void USBDeviceRemoved(object sender, USBClass.USBDeviceEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("            USB DEVICE REMOVED");
        }
    }
}
