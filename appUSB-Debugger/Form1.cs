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

        public static List<SerialPort> serial_port = new List<SerialPort>();
        public List<string> com_port_names = new List<string>();

        public Form1()
        {
            InitializeComponent();

            //USB Connection
            USBPort = new USBClass();
            ListOfUSBDeviceProperties = new List<USBClass.DeviceProperties>();
            USBPort.USBDeviceAttached += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceAttached);
            USBPort.USBDeviceRemoved += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceRemoved);
            USBPort.RegisterForDeviceChange(true, this.Handle);

            find_serial_ports_with_devices();
            init_all_serial_ports();
        }
        private void USBPort_USBDeviceAttached(object sender, USBClass.USBDeviceEventArgs e)
        {
            find_serial_ports_with_devices();
        }

        private void USBPort_USBDeviceRemoved(object sender, USBClass.USBDeviceEventArgs e)
        {
            find_serial_ports_with_devices();
        }


        void init_all_serial_ports()
        {
            //creates a generic port with correct communication properties
            SerialPort port = new SerialPort();
            port.BaudRate = 115200;
            port.Parity = (Parity)Enum.Parse(typeof(Parity), "none", true);
            port.DataBits = 8;
            port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1", false);
            port.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "XonXoff", true);

            System.Diagnostics.Debug.WriteLine("Serial Ports Init:  ");

            ///<summary>
            ///for each port named:
            ///     change the name of the generic port to the named port;
            ///     add the named port to the serial_port list;
            ///</summary>
            for (int i = 0; i < com_port_names.Count; i++)   
            {
                port.PortName = com_port_names[i];
                serial_port.Add(port);
                System.Diagnostics.Debug.WriteLine(serial_port[i].PortName);
            }
        }

        void find_serial_ports_with_devices()
        {
            if (USBClass.GetUSBDevice(VID, PID, ref ListOfUSBDeviceProperties, true))
            {
                System.Diagnostics.Debug.WriteLine("Found com_ports:  ");
                foreach (var device in ListOfUSBDeviceProperties)
                {
                    com_port_names.Add(device.COMPort);

                    System.Diagnostics.Debug.WriteLine(device.COMPort);
                }

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
