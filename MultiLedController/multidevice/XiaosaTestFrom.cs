using MultiLedController.multidevice.impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiLedController.multidevice
{
    public partial class XiaosaTestFrom : Form
    {
        public XiaosaTestFrom()
        {
            InitializeComponent();
        }

        private void TestBtn1_Click(object sender, EventArgs e)
        {
            TransactionManager.GetTransactionManager().SearchDevice("192.168.31.235");
        }
    }
}
