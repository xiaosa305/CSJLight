using MultiLedController.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiLedController
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("开始测试");
            Art_Net_Client client1 = new Art_Net_Client("192.168.1.14", "192.168.1.235");
            Art_Net_Client client2 = new Art_Net_Client("192.168.1.14", "192.168.1.236");
            Art_Net_Client client3 = new Art_Net_Client("192.168.1.14", "192.168.1.237");

        }
    }
}
