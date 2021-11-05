using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.Project
{
    public partial class NewForm : UIForm
    {
        public NewForm()
        {
            InitializeComponent();
        }
        private void NewForm_Load(object sender, EventArgs e)
        {
            Location = MousePosition;
        }
    }
}
