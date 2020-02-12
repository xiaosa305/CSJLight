using MultiLedController.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

            string strHostName = Dns.GetHostName();//获取本机服务器名称
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            string test = ipEntry.AddressList[0].ToString();


            Art_Net_Client client1 = new Art_Net_Client("192.168.1.12", "192.168.1.8",0);
            //Art_Net_Client client2 = new Art_Net_Client("192.168.1.12", "192.168.1.9",1);
            //Art_Net_Client client3 = new Art_Net_Client("192.168.1.12", "192.168.1.10",2);
            //Art_Net_Client client4 = new Art_Net_Client("192.168.1.12", "192.168.1.11", 3);
            Art_Net_Client client5 = new Art_Net_Client("192.168.1.12", "192.168.1.12", 4);
            //Art_Net_Client client6 = new Art_Net_Client("192.168.1.12", "192.168.1.13", 5);
            //Art_Net_Client client7 = new Art_Net_Client("192.168.1.12", "192.168.1.14", 6);
            Art_Net_Client client8 = new Art_Net_Client("192.168.1.12", "192.168.1.15", 7);


        }
    }
}
