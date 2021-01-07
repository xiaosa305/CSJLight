using RecordTools.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RecordTools.test
{
    public partial class XiaosaTestForm : Form
    {
        public XiaosaTestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditMusicRecordFile.GetInstance().ReadRecordFile(@"C:\Users\99729\Desktop\测试\M1.Bin");
        }
    }
}
