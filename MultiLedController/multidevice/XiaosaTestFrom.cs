using MultiLedController.entity;
using MultiLedController.multidevice.impl;
using MultiLedController.utils;
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
        private const string LOCAL_IP = "192.168.31.200";
        private const string SERVER_IP = "192.168.31.200";
        public XiaosaTestFrom()
        {
            InitializeComponent();
        }

        private void TestBtn1_Click(object sender, EventArgs e)
        {
            TransactionManager.GetTransactionManager().SearchDevice(LOCAL_IP);
        }

        private void TestBtn2_Click(object sender, EventArgs e)
        {
            List<ControlDevice> controlDevices = TransactionManager.GetTransactionManager().GetControlDevicesList();
            List<List<string>> ips = new List<List<string>>();
            List<string> virtualIp1 = new List<string>();
            virtualIp1.Add("192.168.31.201");
            virtualIp1.Add("192.168.31.202");
            virtualIp1.Add("192.168.31.203");
            virtualIp1.Add("192.168.31.204");
            virtualIp1.Add("192.168.31.205");
            virtualIp1.Add("192.168.31.206");
            virtualIp1.Add("192.168.31.207");
            virtualIp1.Add("192.168.31.208");
            List<string> virtualIp2 = new List<string>();
            virtualIp2.Add("192.168.31.209");
            virtualIp2.Add("192.168.31.210");
            virtualIp2.Add("192.168.31.211");
            virtualIp2.Add("192.168.31.212");
            virtualIp2.Add("192.168.31.213");
            virtualIp2.Add("192.168.31.214");
            virtualIp2.Add("192.168.31.215");
            virtualIp2.Add("192.168.31.216");
            ips.Add(virtualIp1);
            ips.Add(virtualIp2);
            TransactionManager.GetTransactionManager().AddDevice(controlDevices, ips, SERVER_IP);
        }

        private void TestBtn3_Click(object sender, EventArgs e)
        {
            TransactionManager.GetTransactionManager().Start();
        }
    }
}
