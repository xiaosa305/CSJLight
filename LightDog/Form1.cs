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
        public Form1()
        {
            InitializeComponent();
        }

        private void TestBtn1_Click(object sender, EventArgs e)
        {
            Test.GetInstance().OpenSerialPortAndSearchDevice();
        }

        private void TestBtn2_Click(object sender, EventArgs e)
        {
            Test.GetInstance().SetNewPassword(this.NewPassword.Text);
        }

        private void TestBtn3_Click(object sender, EventArgs e)
        {
            Test.GetInstance().SetTime(this.TIme.Text);
        }

        private void TestBtn4_Click(object sender, EventArgs e)
        {
            Test.GetInstance().Login(this.OldPassword.Text);
        }
    }
}
