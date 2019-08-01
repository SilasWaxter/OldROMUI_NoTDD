using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using USBClassLibrary;

namespace appUSB_Debugger
{
    class SensorClass
    {
        public UInt16[] MacAdd = { 0, 0, 0, 0, 0, 0 };     //Mac-Address of sens
        public SerialPort serialPort;
    }
}
