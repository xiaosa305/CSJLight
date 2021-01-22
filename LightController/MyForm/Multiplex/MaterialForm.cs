using LightController.Ast;
using LightController.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.Multiplex
{
	public partial class MaterialForm : Form
	{
		public MaterialForm()
		{
			InitializeComponent();
		}

		private void lineY1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{

		}

		private void waveY1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{

		}

		private void waveX1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{

		}

		private void cancelButton_Click(object sender, EventArgs e)
		{

		}

		private void enterButton_Click(object sender, EventArgs e)
		{

		}

		private void previewButton_TextChanged(object sender, EventArgs e)
		{

		}

		private void previewButton_Click(object sender, EventArgs e)
		{

		}

		private void materialTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{

		}		

		private void MaterialForm_Load(object sender, EventArgs e)
		{

		}

		private StepWrapper stepWrapper;
		#region 固定通道

		/// <summary>
		/// 事件：点击《添加（固定通道）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdAddButton_Click(object sender, EventArgs e)
		{
			for (int panelIndex = tdFLP.Controls.Count - 1; panelIndex > 0; panelIndex--)
			{
				if (tdFLP.Controls[panelIndex].Controls[0].Text.Contains(tdCB.Text))
				{
					setNotice("此通道已存在，请不要重复添加。", true, true);
					return;
				}
			}

			Panel tdPanel = new Panel
			{
				Location = tdPanelDemo.Location,
				Size = tdPanelDemo.Size,
				BorderStyle = tdPanelDemo.BorderStyle,
			};

			NumericUpDown tdNUD = new NumericUpDown
			{
				Location = tdNUDDemo.Location,
				Maximum = tdNUDDemo.Maximum,
				Size = tdNUDDemo.Size,
				TextAlign = tdNUDDemo.TextAlign,
				Value = stepWrapper.TongdaoList[tdCB.SelectedIndex].ScrollValue,
			};

			Label tdLabel = new Label
			{
				AutoSize = tdLabelDemo.AutoSize,
				Location = tdLabelDemo.Location,
				Size = tdLabelDemo.Size,
				Text = tdCB.Text,
			};

			Button tdDelButton = new Button
			{
				Location = tdDelButtonDemo.Location,
				Size = tdDelButtonDemo.Size,
				Text = "-",
				UseVisualStyleBackColor = tdDelButtonDemo.UseVisualStyleBackColor
			};
			tdDelButton.Click += tdDelButton_Click;

			tdPanel.Controls.Add(tdLabel);
			tdPanel.Controls.Add(tdNUD);
			tdPanel.Controls.Add(tdDelButton);
			tdFLP.Controls.Add(tdPanel);
		}

		/// <summary>
		/// 事件：点击《删除（固定通道）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdDelButton_Click(object sender, EventArgs e)
		{
			tdFLP.Controls.Remove((sender as Button).Parent);
		}

		/// <summary>
		///  事件：点击《清空（固定通道）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdClearButton_Click(object sender, EventArgs e)
		{
			for (int panelIndex = tdFLP.Controls.Count - 1; panelIndex > 0; panelIndex--)
			{
				tdFLP.Controls.RemoveAt(panelIndex);
			}
		}

		/// <summary>
		/// 辅助方法：生成固定通道列表
		/// </summary>
		/// <returns></returns>
		private Dictionary<string, int> generateTdDict()
		{

			Dictionary<string, int> tdDict = new Dictionary<string, int>();
			for (int panelIndex = 1; panelIndex < tdFLP.Controls.Count; panelIndex++)
			{
				Panel tdPanel = tdFLP.Controls[panelIndex] as Panel;
				tdDict.Add(
					tdPanel.Controls[0].Text.Trim(),
					decimal.ToInt32((tdPanel.Controls[1] as NumericUpDown).Value)
				);
			}

			return tdDict;
		}

		#endregion

		#region 通用方法

		/// <summary>
		/// 辅助方法：设置提醒
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow, bool isTranslate)
		{
			if (isTranslate)
			{
				msg = LanguageHelper.TranslateSentence(msg);
			}

			myStatusLabel.Text = msg;
			if (msgBoxShow)
			{
				MessageBox.Show(msg);
			}
		}

		/// <summary>
		/// 辅助方法：某些控件的文本发生变化时， 需要进行翻译
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someControl_TextChanged(object sender, EventArgs e)
		{
			LanguageHelper.TranslateControl(sender as Control);
		}

		#endregion

	}
}
