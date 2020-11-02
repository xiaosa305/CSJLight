using MultiLedController.multidevice.multidevicepromax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiLedController.MyForm
{
    public partial class XiaosaTestForm : Form
    {
        private VirtualProClientsManager manager { get; set; }

        public XiaosaTestForm()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            this.Stop_Click(null, null);
            List<String> ips = new List<string>();
            ips.Add("192.168.31.236");
            int space = 6;
            int ledInterface = 8;
            int control = 1;
            manager = new VirtualProClientsManager("192.168.31.235", "192.168.31.235", ips, space, ledInterface, control);
            manager.Start();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if (manager != null)
            {
                manager.Stop();
            }
        }

        private void StartDebug_Click(object sender, EventArgs e)
        {
            if (manager != null)
            {
                manager.StartDebug(this.ShowDebugCount);
            }
        }

        private void StopDebug_Click(object sender, EventArgs e)
        {
            if (manager != null)
            {
                manager.StopDebug();
            }
        }

        private void StartRecord_Click(object sender, EventArgs e)
        {
            if (manager != null)
            {
                manager.StartRecord(@"D:\XiaoSa\Test\SC000.bin", @"D:\XiaoSa\Test\csj.scu", ShowRecordCount);
            }
        }

        private void StopRecord_Click(object sender, EventArgs e)
        {
            if (manager != null)
            {
                manager.StopRecord();
            }
        }

        private void ShowDebugCount(int count)
        {
            //this.debugcount.Text = count.ToString();
            Console.WriteLine("debug: "+ count);
        }

        private void ShowRecordCount(int count)
        {
            //this.recordcount.Text = count.ToString();
            Console.WriteLine("record: " + count);

        }
    }
}
