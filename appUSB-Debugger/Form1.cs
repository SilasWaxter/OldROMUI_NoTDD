using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Security.Cryptography;     //used for generating 
using USBClassLibrary;                  //used for usb communication
using System.IO.Ports;
using System.Threading;

namespace appUSB_Debugger
{
    public partial class Form1 : Form
    {
        //ROM network variables
        public UInt16[] MA_sens = { 0, 0, 0, 0, 0, 0 };     //Mac-Address of sens
        public UInt16[] MA_hub = { 0, 0, 0, 0, 0, 0 };     //Mac-Address of hub
        public byte[] key;

        //USB variables
        private uint VID = 0x1A86, PID = 0x7523;
        private USBClassLibrary.USBClass USBPort;
        private System.Collections.Generic.List<USBClassLibrary.USBClass.DeviceProperties> ListOfUSBDeviceProperties;
        int?[] serial_port_number = { null, null, null};   //? in front of int makes it nullable

        static SerialPort serial_port;
        public Form1()
        {
            InitializeComponent();

            serial_port = new SerialPort();
            serial_port.PortName = "COM6";
            serial_port.BaudRate = 115200;
            serial_port.Parity = (Parity)Enum.Parse(typeof(Parity), "none", false);
            serial_port.DataBits = 8;
            serial_port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1", false);
            serial_port.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "xon/xoff", false);

            System.Diagnostics.Debug.Write(serial_port);

            //USB Connection
            USBPort = new USBClass();
            ListOfUSBDeviceProperties = new List<USBClass.DeviceProperties>();
            USBPort.USBDeviceAttached += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceAttached);
            USBPort.USBDeviceRemoved += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceRemoved);
            USBPort.RegisterForDeviceChange(true, this.Handle);

            find_serial_devices();
        }
        private void USBPort_USBDeviceAttached(object sender, USBClass.USBDeviceEventArgs e)
        {
            find_serial_devices();
        }

        private void USBPort_USBDeviceRemoved(object sender, USBClass.USBDeviceEventArgs e)
        {
            find_serial_devices();
        }

        void find_serial_devices()
        {
            if (USBClass.GetUSBDevice(VID, PID, ref ListOfUSBDeviceProperties, true))
            {

            }
        }

        void generate_key()
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.KeySize = 128;
                aes.GenerateKey();
                aes.Key.CopyTo(key, 0);     //copy aes.key to [byte[] key] starting at 0 index
                System.Diagnostics.Debug.Write(aes.Key.Length);
                foreach (byte b in key) System.Diagnostics.Debug.Write(b);  //write key to debugger
            }
        }
    }
}
