using LightController.Ast;
using LightEditor.Ast;
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
		private int tdIndex;

		public SAUseForm(MainFormBase mainForm, LightAst la,int tdIndex ,string tdName)
		{
			this.mainForm = mainForm;			
			this.tdIndex = tdIndex;

			InitializeComponent();
					   
			Text = tdName +"【" + la.LightType + "(" + la.LightAddr + ")】" ;

			try
			{
				for (int saIndex = 0; saIndex < la.SawList[tdIndex].SaList.Count; saIndex++)
				{
					SA sa = la.SawList[tdIndex].SaList[saIndex];
					Button saButton = new Button
					{
						Text = sa.SAName,
						Size = new Size(94,23),
						Tag = tdIndex + "*" + sa.StartValue,
						UseVisualStyleBackColor = true
					};
					saButton.Click += new EventHandler(saButton_Click);
					saToolTip.SetToolTip(saButton, sa.SAName + "\n" + sa.StartValue + " - " + sa.EndValue);
					saFlowLayoutPanel.Controls.Add(saButton);
				}				
			}
			catch (Exception ex)
			{
				MessageBox.Show("添加子属性按键出现异常:\n" + ex.Message);
			}
		}

		private void SAUseForm_Load(object sender, EventArgs e)
		{
			Location = new Point(MousePosition.X+40, MousePosition.Y);
		}

		/// <summary>
		/// 事件：点击《saButton》按钮组的任意按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saButton_Click(object sender, EventArgs e)
		{
			//saButtonClick(sender);
		}
	}
}
