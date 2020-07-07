using LightDog.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightDog
{
    public partial class Form1 : Form
    {
        private Order CurrentOrder { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void TestBtn1_Click(object sender, EventArgs e)
        {
            this.CurrentOrder = Order.Check;
            Test.GetInstance().OpenSerialPortAndSearchDevice(this.Completed,this.Error);
        }

        private void TestBtn2_Click(object sender, EventArgs e)
        {
            if (this.NewPassword.Text.Length != 8)
            {
                MessageBox.Show("密码不是8个字符");
            }
            else
            {
                Test.GetInstance().SetNewPassword(this.NewPassword.Text, this.Completed, this.Error);
            }
        }

        private void TestBtn3_Click(object sender, EventArgs e)
        {
            if (uint.TryParse(this.TIme.Text, out uint result))
            {
                if (result >= 0)
                {
                    Test.GetInstance().SetTime(this.TIme.Text, this.Completed, this.Error);
                    return;
                }
            }
            MessageBox.Show("时间输入错误");
        }

        private void TestBtn4_Click(object sender, EventArgs e)
        {
            if (this.OldPassword.Text.Length == 0)
            {
                MessageBox.Show("密码不能为空");
            }
            else
            {
                Test.GetInstance().Login(this.OldPassword.Text, this.Completed, this.Error);
            }
        }

        public void Completed(Object obj,string msg)
        {
            switch (this.CurrentOrder)
            {
                case Order.SetPassword:
                    break;
                case Order.SetTime:
                    break;
                case Order.Check:
                    this.TestBtn4.Enabled = true;
                    break;
                case Order.Login:
                    this.TestBtn3.Enabled = true;
                    this.TestBtn2.Enabled = true;
                    break;
                case Order.Null:
                default:
                    break;
            }
            MessageBox.Show(msg);
        }

        public void Error(Object obj, string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
