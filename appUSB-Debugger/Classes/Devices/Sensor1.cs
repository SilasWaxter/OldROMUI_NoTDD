using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using USBClassLibrary;

namespace appUSB_Debugger
{
    public static class Sensor1
    {
        public static double[] q = { 0, 0, 0, 0 };
        public static string sensorSpecifier = "S1";
        public static Device device;
    }
}
