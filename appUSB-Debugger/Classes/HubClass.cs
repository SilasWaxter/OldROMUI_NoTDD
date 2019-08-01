using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using USBClassLibrary;

namespace appUSB_Debugger
{
    class HubClass
    {
        public UInt16[] MacAdd = { 0, 0, 0, 0, 0, 0 };     //Mac-Address of hub
        public SerialPort serialPort;
    }
}
