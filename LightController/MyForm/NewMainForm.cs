using LightController.Common;
using OtherTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class NewMainForm : Form
	{
		public static IList<string> AllFrameList;
		public static int FrameCount;

		Image buttonImg1 = global::LightController.Properties.Resources.Ok3w_Net图标13;
		Image buttonImg2 = global::LightController.Properties.Resources.Ok3w_Net图标1;

		public NewMainForm()
		{
			InitializeComponent();

			#region 皮肤相关代码
			IniFileAst iniFileAst = new IniFileAst(Application.StartupPath + @"\GlobalSet.ini");
			string skin = iniFileAst.ReadString("SkinSet", "skin", "");
			if (!String.IsNullOrEmpty(skin))
			{
				this.skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + skin;
			}
			DirectoryInfo fdir = new DirectoryInfo(Application.StartupPath + "\\irisSkins");
			try
			{
				FileInfo[] file = fdir.GetFiles();
				if (file.Length > 0)
				{
					skinComboBox.Items.Add("更改皮肤");
					foreach (var item in file)
					{
						if (item.FullName.EndsWith(".ssk"))
						{
							skinComboBox.Items.Add(item.Name.Substring(0, item.Name.Length - 4));
						}
					}
					skinComboBox.SelectedIndex = 0;
					skinComboBox.Show();					
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			#endregion

			#region 几个下拉框的初始化及赋值
			//添加FramList文本中的场景列表
			AllFrameList = TextAst.Read(Application.StartupPath + @"\FrameList.txt");

			// 场景选项框			
			foreach (string frame in AllFrameList)
			{
				frameComboBox.Items.Add(frame);
			}
			FrameCount = AllFrameList.Count;
			if (FrameCount == 0)
			{
				MessageBox.Show("FrameList.txt中的场景不可为空，否则软件无法使用，请修改后重启。");
				exit();
			}
			frameComboBox.SelectedIndex = 0;

			//模式选项框
			modeComboBox.Items.AddRange(new object[] { "常规模式", "音频模式" });
			modeComboBox.SelectedIndex = 0;

			// 《统一跳渐变》numericUpDown不得为空，否则会造成点击后所有通道的changeMode形式上为空（不过Value不是空）
			unifyChangeModeComboBox.SelectedIndex = 1;

			#endregion


		}

		private void exit()
		{
			System.Environment.Exit(0);
		}


		/// <summary>
		/// 事件：点击《退出程序》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			exit();
		}

		/// <summary>
		/// 辅助方法：退出程序
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewMainForm_Load(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// 事件：点击《灯库编辑》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightLibraryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(Application.StartupPath + @"\LightEditor.exe");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 事件：点击《新版配置工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//// 若要进入《其他工具》，应该先将连接断开
			//if (isConnected)
			//{
			//	connectSkinButton_Click(null, null);
			//}

			new NewToolsForm(this).ShowDialog();
		}

		private void skinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string sskName = skinComboBox.Text;
			if (String.IsNullOrEmpty(sskName) || sskName.Equals("更改皮肤")) {
				this.skinEngine1.Active = false;
				return;
			}
			this.skinEngine1.Active = true;
			this.skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + sskName + ".ssk";
		}
	}
}
