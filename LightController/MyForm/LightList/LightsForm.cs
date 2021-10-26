
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LightController.Ast;
using LightController.MyForm;
using LightController.Common;
using LightController.Ast.Enum;

namespace LightController
{
	public partial class LightsForm : System.Windows.Forms.Form
	{
		public const int MAX_TD = 512;		
		private int minNum = 1; //每次new LightsAstForm的时候，需要填入的最小值；也就是当前所有灯具通道占用的最大值+1
		private IList<LightAst> lightAstList = new List<LightAst>();		
		private MainFormBase mainForm; 
		private List<LightsChange> changeList ;  //DOTO 211026 定义changeList
			 
		public LightsForm(MainFormBase mainForm, IList<LightAst> lightAstListFromMain)
		{
			InitializeComponent();
			this.mainForm = mainForm;			

			// 1. 生成左边的灯具列表，树状形式		
			string path = mainForm.SavePath  + @"\LightLibrary" ;
			if (Directory.Exists(path))
			{
				string[] dirs = Directory.GetDirectories(path);
				foreach (string dir in dirs)
				{
					DirectoryInfo di = new DirectoryInfo(dir);
					TreeNode treeNode = new TreeNode(di.Name);

					//由ini文件生成子节点
					FileInfo[] files = di.GetFiles();
					foreach (FileInfo file in files)
					{
						string fileName = file.Name;
						if (fileName.EndsWith(".ini"))
						{
							TreeNode node = new TreeNode(
								fileName.Substring(0, fileName.IndexOf("."))
							);
							treeNode.Nodes.Add(node);
						}
					}
					this.skinTreeView1.Nodes.Add(treeNode);
				}
				this.skinTreeView1.ExpandAll();				
			}
						
			// 2.只有加载旧项目（已有LightAst列表）时，才加载lightAstList到右边
			if (lightAstListFromMain != null && lightAstListFromMain.Count > 0) {
				foreach (LightAst laOld in lightAstListFromMain)
				{
					lightAstList.Add(new LightAst(laOld));
				}
				foreach (LightAst la in lightAstList)
				{
					addListViewItem(la.LightName, la.LightType, la.LightAddr, la.LightPic);
					minNum = la.EndNum + 1;
				}				
			}

			//DOTO 211026 初始化changeList 
			changeList = new List<LightsChange>();
		}
			
		/// <summary>
		///  窗口Load方法：作用是初始化窗体位置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LightsForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			lightsListView.HideSelection = true;
			LanguageHelper.InitForm(this);
			LanguageHelper.TranslateListView(lightsListView);
		}

		/// <summary>
		/// 事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		///  事件：关闭窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LightsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		///  事件：点击《添加》按钮：添加新灯具
		///  1.需选中左边的一个灯具（灯库），点击添加
		///  2.打开一个NewForm的新实例，在NewForm中填好参数后回调AddListView方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addLightButton_Click(object sender, EventArgs e)
		{
			if (skinTreeView1.SelectedNode == null || skinTreeView1.SelectedNode.Level == 0)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("请先选择灯具。"));
			} else {
				if (skinTreeView1.SelectedNode.Parent != null)
				{
					string fullPath = mainForm.SavePath + @"\LightLibrary\" + skinTreeView1.SelectedNode.FullPath + ".ini";
					LightsAstForm lightsAstForm = new LightsAstForm(this, fullPath, minNum);
					lightsAstForm.ShowDialog();
				}
			}			
		}

		/// <summary>
		/// 事件：双击灯具进行修改
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsListView_DoubleClick(object sender, EventArgs e)
		{
			int lightIndex = lightsListView.SelectedIndices[0];
			LightsEditForm lightsEditForm = new LightsEditForm(this, lightAstList[lightIndex], lightIndex);
			lightsEditForm.ShowDialog();
		}

		/// <summary>
		/// 事件：点击《删除灯具》的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteLightButton_Click(object sender, EventArgs e)
		{
			if (lightsListView.SelectedIndices.Count == 0)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("请先选择要删除的灯具。"));
			}
			else
			{
				// 选择多个灯具情况下的删除方法：通过item来删除数据
				foreach (ListViewItem item in lightsListView.SelectedItems)
				{
					//DOTO 211026 新增changeList项，在删除灯具时
					changeList.Add(
						new LightsChange()
						{
							Operation = EnumOperation.DELETE,
							LightIndex = item.Index,
						}
					);

					lightAstList.RemoveAt(item.Index);	
					item.Remove();
				}
				lightsListView.Refresh();
			}
		}

		/// <summary>
		///  事件：点击《确认》后，添加lightAstList到mainForm去，并进行相关操作
		/// --用此lightAstList替代mainForm中的原lightAstList，并顺便删减lightWrapperList和ListView中的灯具
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			// 1.当点击确认时，应该将所有的listViewItem 传回到mainForm里。
			//mainForm.ReBuildLightList(lightAstList);

			//DOTO 211026
			//当修改了灯具列表后，必须保存工程：
			//1. 若点了取消，则还保持在当前界面return；
			//2.点了是，则执行操作；
			//3.未修改和点了是(2)之后，统一都要激活mainForm
			if (changeList != null && changeList.Count > 0) {
				if (DialogResult.Cancel == MessageBox.Show(
						LanguageHelper.TranslateSentence("修改灯具列表后，需保存工程变化才能生效，是否立刻保存？"),
						"保存工程？",
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Question)) {
					return;
				}
				mainForm.ReBuildLightList2(changeList);
			}
			Dispose();
			mainForm.Activate();			
		}	

		/// <summary>
		///  辅助方法：添加数据到ListView中；主要给NewForm回调使用；添加后minNum设成endNum
		/// </summary>
		/// <param name="lightPath"></param>
		/// <param name="lightName"></param>
		/// <param name="lightType"></param>
		/// <param name="lightAddr"></param>
		/// <param name="lightPic"></param>
		/// <param name="startNum"></param>
		/// <param name="endNum"></param>
		/// <param name="lightCount"></param>
		internal void AddListViewAndLightAst(String lightPath,string lightName, string lightType, 
					string lightAddr,string lightPic,int startNum,int endNum,int lightCount)
		{
			// 新增时，1.直接往listView加数据，
			addListViewItem(lightName, lightType, lightAddr, lightPic);

			// 2.往lightAstList添加新的数据
			LightAst newLightAst = new LightAst()
			{
				LightAddr = lightAddr,
				LightName = lightName,
				LightType = lightType,
				LightPic = lightPic,
				LightPath = lightPath,
				StartNum = startNum,
				EndNum = endNum,
				Count = lightCount
			};

			lightAstList.Add(newLightAst);

			// 3.设置minNum的值 			
			minNum = endNum + 1;
			if( minNum>512 ) {
				MessageBox.Show(LanguageHelper.TranslateSentence("当前工程已经到达DMX512地址上限，请谨慎设置！"));
				minNum = 512;
			}

			//DOTO 211026 新增changeList项，在新增灯具时
			changeList.Add(new LightsChange()
			{
				Operation= EnumOperation.ADD,
				NewLightAst = newLightAst
			});			
		}
			   		
		/// <summary>
		///  辅助方法：供LightsEditForm回调使用
		/// </summary>
		/// <param name="lightIndex"></param>
		/// <param name="startNum"></param>
		internal bool UpdateLight(int lightIndex, int startNum)
		{
			int endNum = startNum + lightAstList[lightIndex].Count - 1;
			if (endNum > MAX_TD) {
				MessageBox.Show(LanguageHelper.TranslateSentence("设置后的最后地址超过了DMX512的上限值，请重新设置。"));
				return false;
			}
			else
			{
				// 1.修改lightAstList
				lightAstList[lightIndex].StartNum = startNum;
				lightAstList[lightIndex].EndNum = endNum;
				lightAstList[lightIndex].LightAddr = startNum + "-" + endNum;

				//DOTO 211026 新增changeList项，在修改地址时
				changeList.Add(new LightsChange()
				{
					Operation = EnumOperation.UPDATE,
					LightIndex = lightIndex,
					NewLightAst = lightAstList[ lightIndex],
				});

				// 2.修改lightListView
				lightsListView.Items[lightIndex].SubItems[2].Text = lightAstList[lightIndex].LightAddr;
				return true;
			}			
		}

		/// <summary>
		///  辅助方法：添加item到ListView中，需要一些参数
		/// </summary>
		/// <param name="lightName"></param>
		/// <param name="lightType"></param>
		/// <param name="lightAddr"></param>
		/// <param name="lightPic"></param>
		private void addListViewItem(string lightName, string lightType, string lightAddr, string lightPic)
		{
			ListViewItem item = new ListViewItem(lightName);
			item.SubItems.Add(lightType);
			item.SubItems.Add(lightAddr);
			item.ImageKey = lightPic;
			lightsListView.Items.Add(item);
		}
		               
        /// <summary>
        /// 辅助方法：检测传进来的起始地址和截止地址（同时添加多个灯具时，也会有这两个地址【第一个灯的起始地址和最后一个灯的截止地址】）
        /// ，是否已被当前灯具所占用；
        ///     若传进来的lightIndex==-1；则表示新加灯具，否则为修改旧灯具，先从表中删除旧灯具
         /// </summary>
        /// <param name="startAddr"></param>
        /// <param name="endAddr"></param>
        public bool CheckAddrAvailale(int lightIndex, int startAddr  , int endAddr) {
            bool result = true;

            List<int> addrList = new List<int>();
            for (int i = 0; i < lightAstList.Count; i++)
            {
                if ( i != lightIndex) {
                    LightAst la = lightAstList[i];                    
                    for (int j = la.StartNum; j <= la.EndNum; j++)
                    {                        
                        addrList.Add(j);
                    }                    
                }
            }           

            for (int addr = startAddr ; addr <= endAddr; addr++)
            {
                if (addrList.Contains(addr)) {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine(changeList);
        }
    }
}
