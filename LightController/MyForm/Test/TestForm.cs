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
        private void TestPicture()
        {
            float startX = 50;
            float startY = 50;
            float triangleSide = 50;
            float triangleHSide = (float)(Math.Sqrt(triangleSide * triangleSide - (triangleSide / 2) * (triangleSide / 2)));
            Graphics graphics = PictureBox.CreateGraphics();
            graphics.Clear(Color.White);
            Pen pen1 = new Pen(Color.Black, 1);

            Point point1 = new Point((int)startX, (int)startY);
            Point point2 = new Point((int)(startX + triangleSide), (int)startY);
            Point point3 = new Point((int)(startX + triangleSide + triangleSide / 2), (int)(startY + triangleHSide));
            Point point4 = new Point((int)(startX + triangleSide), (int)(startY + triangleHSide * 2));
            Point point5 = new Point((int)startX, (int)(startY + triangleHSide * 2));
            Point point6 = new Point((int)(startX - triangleSide / 2), (int)(startY + triangleHSide));

            graphics.DrawLine(pen1, point1, point2);
            graphics.DrawLine(pen1, point2, point3);
            graphics.DrawLine(pen1, point3, point4);
            graphics.DrawLine(pen1, point4, point5);
            graphics.DrawLine(pen1, point5, point6);
            graphics.DrawLine(pen1, point6, point1);


            ////第一条边
            //graphics.DrawLine(pen1, startX, startY, startX + triangleSide, startY);
            ////第二条边
            //graphics.DrawLine(pen1, startX + triangleSide, startY, startX + triangleSide + triangleSide / 2, startY + triangleHSide);
            ////第三条边
            //graphics.DrawLine(pen1, startX + triangleSide + triangleSide / 2, startY + triangleHSide, startX + triangleSide, startY + triangleHSide * 2);
            ////第四条边
            //graphics.DrawLine(pen1, startX + triangleSide, startY + triangleHSide * 2, startX, startY + triangleHSide * 2);
            ////第五条边
            //graphics.DrawLine(pen1, startX, startY + triangleHSide * 2, startX - triangleSide / 2, startY + triangleHSide);
            ////第六条边
            //graphics.DrawLine(pen1, startX - triangleSide / 2, startY + triangleHSide, startX, startY);
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
        private void StartDrawPicture_Click(object sender, EventArgs e)
        {
            TestPicture();
        }
        private void ClearPictureBox_Click(object sender, EventArgs e)
        {
            Graphics graphics = PictureBox.CreateGraphics();
            graphics.Clear(Color.White);
        }




        private void NewConnectedTestBtn_Click(object sender, EventArgs e)
        {
            XiaosaTest.GetInstance().NewConnectTest();
        }
    }
}
