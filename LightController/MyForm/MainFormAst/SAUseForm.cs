using LightController.Ast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.MainFormAst
{
	public partial class SAUseForm : Form
	{
		private MainFormBase mainForm;

		public SAUseForm(MainFormBase mainForm, LightAst la)
		{
			InitializeComponent();

			Text = la.LightType + "(" + la.LightAddr + ")";

		}
	}
}
