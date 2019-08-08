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
    public partial class NewHubFound : Form
    {
        public string returnText { get; set; }
        public NewHubFound()
        {
            InitializeComponent();
        }

        private void rbtnReplace_Click(object sender, EventArgs e)
        {
            returnText = "replace";
        }

        private void rbtnIgnore_Click(object sender, EventArgs e)
        {
            returnText = "ignore";
        }
    }
}
