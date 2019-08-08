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
    public partial class NewSensorFound : Form
    {
        public string returnText = "ignore";

        public NewSensorFound()
        {
            InitializeComponent();
        }

        private void rbtnReplaceSensor1_Selected(object sender, EventArgs e)
        {
            returnText = "replaceSensor1";
        }

        private void rbtnReplaceSensor2_Selected(object sender, EventArgs e)
        {
            returnText = "replaceSensor2";
        }

        private void rbtnIgnore_Click(object sender, EventArgs e)
        {
            returnText = "ignore";
        }
    }
}
