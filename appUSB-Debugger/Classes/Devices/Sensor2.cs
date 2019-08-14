using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using USBClassLibrary;

namespace appUSB_Debugger
{
    public static class Sensor2
    {
        public static double[] q = { 0, 0, 0, 0 };
        public static string sensorSpecifier = "S2";
        public static Device device;
    }
}
