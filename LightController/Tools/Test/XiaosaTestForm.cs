using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.Tools.Test
{
    public partial class XiaosaTestForm : Form
    {
        public XiaosaTestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DmxRecordTest.GetInstance().Start();
        }
    }
}
