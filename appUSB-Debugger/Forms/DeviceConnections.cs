using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appUSB_Debugger
{
    public partial class DeviceConnections : Form
    {
        public DeviceConnections()
        {
            InitializeComponent();
        }

        private async void Pair_ClickAsync(object sender, EventArgs e)
        {
            Serial.Write(Hub.device, "~stopQuatSender~");
            while (!Serial.ReceivedMessageBool(Hub.device, "~stopingQuatSender~")) ;

            //Create a task that completes when/if a Sensor1 device is classified.
            Task s1PluggedIn = Task.Factory.StartNew(() => { while (Sensor1.device == null) ; });
            await s1PluggedIn;

            //Start Sensor1 pair.
            Pairing.Pair(Sensor1.device);

            //Create a task that completes when/if a Sensor2 device is classifed
            Task s2PluggedIn = Task.Factory.StartNew(() => { while (Sensor2.device == null) ; });
            await s2PluggedIn;

            //Start Sensor2 pair.
            Pairing.Pair(Sensor2.device);

            //Create a task that completes when/if a Hub device is classified
            Task hubPluggedIn = Task.Factory.StartNew(() => { while (Hub.device == null) ; });
            await hubPluggedIn;

            //Hub Pair is last.  Needs both Sensor 1 and Sensor 2 MacAdd.
            Pairing.Pair(Hub.device);
        }
    }
}
