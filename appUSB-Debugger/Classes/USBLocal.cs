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
        private USBClassLibrary.USBClass PortUSB;
        private System.Collections.Generic.List<USBClassLibrary.USBClass.DeviceProperties> ListOfUSBDeviceProperties;

        public IntPtr Handle { get; private set; }

        public void SearchDevices()
        {
            PortUSB = new USBClassLibrary.USBClass();
            ListOfUSBDeviceProperties = new List<USBClassLibrary.USBClass.DeviceProperties>();
            PortUSB.USBDeviceAttached += new USBClassLibrary.USBClass.USBDeviceEventHandler(USBDeviceAttached);
            PortUSB.USBDeviceRemoved += new USBClassLibrary.USBClass.USBDeviceEventHandler(USBDeviceRemoved);
            PortUSB.RegisterForDeviceChange(true, this.Handle);
        }

        public void DevicesCOMPorts(List<string> COMPorts)
        {
            if (USBClassLibrary.USBClass.GetUSBDevice(VID, PID, ref ListOfUSBDeviceProperties, true))
            {
                System.Diagnostics.Debug.WriteLine("Found com_ports:  ");

                foreach (var device in ListOfUSBDeviceProperties)
                {
                    COMPorts.Add(device.COMPort);

                    System.Diagnostics.Debug.WriteLine(device.COMPort);
                }
            }
        }

        private void USBDeviceAttached(object sender, USBClassLibrary.USBClass.USBDeviceEventArgs e)
        {

        }

        private void USBDeviceRemoved(object sender, USBClassLibrary.USBClass.USBDeviceEventArgs e)
        {

        }
    }
}
