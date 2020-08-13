using MultiLedController.entity;
using MultiLedController.multidevice.impl;
using MultiLedController.multidevice.newmultidevice;
using MultiLedController.utils;
using MultiLedController.utils.impl;
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
            Dictionary<string, ControlDevice> controlDevices = TransactionManager.GetTransactionManager().GetControlDevicesList();
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
            TransactionManager.GetTransactionManager().AddDevice(controlDevices.Values.ToList(), ips, SERVER_IP);
            LogTools.Debug(Constant.TAG_XIAOSA, "添加设备成功");
        }

        private void TestBtn3_Click(object sender, EventArgs e)
        {
            TransactionManager.GetTransactionManager().StartAllDeviceReceiveDmxData();
        }

        private void TestBtn4_Click(object sender, EventArgs e)
        {
            TransactionManager.GetTransactionManager().StartAllDeviceRecode();
        }

        private void TestBtn5_Click(object sender, EventArgs e)
        {
            TransactionManager.GetTransactionManager().StopAllDeviceRecode();
        }

        private void TestBtn6_Click(object sender, EventArgs e)
        {
            TransactionManager.GetTransactionManager().StopAllDeviceReceiveDmxData();
        }

        private void TestBtn7_Click(object sender, EventArgs e)
        {
            TransactionManager.GetTransactionManager().StartAllDeviceDebug();
        }

        private void TestBtn8_Click(object sender, EventArgs e)
        {
            TransactionManager.GetTransactionManager().StopAllDeviceDebug();
        }

        NewVirtualDevice virtualDevice;
        private void TestBtn_Click(object sender, EventArgs e)
        {
            List<string> ips = new List<string>();
            ips.Add("192.168.31.201");
            ips.Add("192.168.31.202");
            ips.Add("192.168.31.203");
            ips.Add("192.168.31.204");
            ips.Add("192.168.31.205");
            ips.Add("192.168.31.206");
            ips.Add("192.168.31.207");
            ips.Add("192.168.31.208");
            //ips.Add("192.168.31.209");
            //ips.Add("192.168.31.210");
            //ips.Add("192.168.31.211");
            //ips.Add("192.168.31.212");
            //NewVirtualDevice virtualDevice = new NewVirtualDevice(8, ips, 6, 1, "192.168.31.135", "192.168.31.200");
            //virtualDevice = new NewVirtualDevice(8, ips, 4, 1, "192.168.31.135", "192.168.31.200");
            virtualDevice = new NewVirtualDevice(8, ips, 4, 1, "192.168.31.200", "192.168.31.200");

            virtualDevice.Test();
        }

        private void stoprecord_Click(object sender, EventArgs e)
        {
            if (virtualDevice.IsRunning())
            {
                virtualDevice.StopRecord();
            }
        }

        private void StopDeviceBtn_Click(object sender, EventArgs e)
        {
            if (virtualDevice.IsRunning())
            {
                virtualDevice.Close();
            }
        }
    }
}
