using LightController.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.Test
{
	public partial class TestForm : Form
	{
		public TestForm()
		{
            InitializeComponent();
            SetSerialPortBox();
            SetSerialPortStartText();
		}

        private void SetSerialPortStartText()
        {
            if (PlayTools.GetInstance().IsTest)
            {
                this.StartTestMode.Text = "关闭测试模块";
            }
            else
            {
                this.StartTestMode.Text = "启动测试模块";
            }
        }

        private void SetSerialPortBox()
        {
            if (PlayTools.GetInstance().GetTestSerialPortNameList() != null)
            {
                this.SerialPortBox.Items.AddRange(PlayTools.GetInstance().GetTestSerialPortNameList());
                this.SerialPortBox.SelectedIndex = 0;
            }
        }

        private void ReLoadSerialPortBox_Click(object sender, EventArgs e)
        {
            this.SerialPortBox.Items.Clear();
            if (PlayTools.GetInstance().GetTestSerialPortNameList() != null)
            {
                this.SerialPortBox.Items.AddRange(PlayTools.GetInstance().GetTestSerialPortNameList());
                this.SerialPortBox.SelectedIndex = 0;
            }
        }

       

        private void StartTestMode_Click(object sender, EventArgs e)
        {
            if (PlayTools.GetInstance().IsTest)
            {
                PlayTools.GetInstance().CloseTestMode();
                this.StartTestMode.Text = "启动测试模块";
            }
            else
            {
                PlayTools.GetInstance().StartTestMode(this.SerialPortBox.Text);
                this.StartTestMode.Text = "关闭测试模块";
            }
        }
    }
}
