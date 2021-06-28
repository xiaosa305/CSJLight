using LightController.Ast;
using LightController.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.Multiplex
{
	public partial class MaterialUseForm : Form
	{
		private MainFormBase mainForm ;
		private MaterialAst complexMaterial;  // 复合的素材，预览及调用时使用（结合了四个小项素材后的情况）

		// 为一些数据设置一些默认值，使得初次进来时，可以渲染各个Tab页 及 其它的输入项（步时间等）
		private int oldMode  = -1; 
		private string oldLightType = "";
		private decimal oldEachStepTime = .0m;

		private StepWrapper stepTemplate;
		
		// 调用素材相关
		private string generalStr = @"\通用\";
		private string specialStr;
		private string materialPath;

		// 调色素材相关		
		private int soundStepTime = 10; 
		private MaterialAst colorMaterial ;  
		private MaterialAst singleColorMat;  // singleColorMat，才能保存之前的数据
		private IList<string> colorTdNameList;     
		private string dimmerStr = LanguageHelper.TranslateWord("总调光");
		private string rStr = LanguageHelper.TranslateWord("红");
		private string gStr = LanguageHelper.TranslateWord("绿");
		private string bStr = LanguageHelper.TranslateWord("蓝");					
		private int colorCount = 0; //这个值不会轻易被改变，只有在【增删】Color块时才会发生变化；且因为其值=Controls.Count - 1, 不保存为专用变量，则引用容易出错。
		private int colorSelectedIndex = -1; //选中的色块，只有在【选增删】Color块时才会发生变化；其值因为由1开始（有个隐藏的panelDemo）,刚好符合明面的色块序号。

		// 动作素材相关
		private int commonStepTime = 50;
		private MaterialAst actionMaterial ;
		IList<string> actionTdNameList;
		private string xStr = LanguageHelper.TranslateWord("X轴");
		private string yStr = LanguageHelper.TranslateWord("Y轴");		
		
		public MaterialUseForm(MainFormBase mainForm)
		{			
			InitializeComponent();
			this.mainForm = mainForm;

			// 内置动作和调色的tdNameList，是固定的。
			actionTdNameList = new List<string> { xStr, yStr };
			colorTdNameList = new List<string> { dimmerStr, rStr, gStr, bStr }; 

			#region 为各个固定的NUD或TrackBar添加滚轮监听

			tgNUD.MouseWheel += someNUD_MouseWheel ;
			tgTrackBar.MouseWheel += someTrackBar_MouseWheel ;
			
			StNumericUpDown.MouseWheel += someNUD_MouseWheel;
			
			lineXNumericUpDown.MouseWheel += someNUD_MouseWheel;
			lineY1NumericUpDown.MouseWheel += someNUD_MouseWheel;
			lineY2NumericUpDown.MouseWheel += someNUD_MouseWheel;
			linePhaseNumericUpDown.MouseWheel += someNUD_MouseWheel;

			circleXNumericUpDown.MouseWheel += someNUD_MouseWheel;
			circleYNumericUpDown.MouseWheel += someNUD_MouseWheel;
			circlePhaseNumericUpDown.MouseWheel += someNUD_MouseWheel;

			scXNumericUpDown.MouseWheel += someNUD_MouseWheel;
			scYNumericUpDown.MouseWheel += someNUD_MouseWheel;
			scPhaseNumericUpDown.MouseWheel += someNUD_MouseWheel;

			waveX1NumericUpDown.MouseWheel += someNUD_MouseWheel;
			waveX2NumericUpDown.MouseWheel += someNUD_MouseWheel;
			waveY1NumericUpDown.MouseWheel += someNUD_MouseWheel;
			waveY2NumericUpDown.MouseWheel += someNUD_MouseWheel;
			waveTimesNumericUpDown.MouseWheel += someNUD_MouseWheel;

			eightYNumericUpDown.MouseWheel += someNUD_MouseWheel;
			eightPhaseNumericUpDown.MouseWheel += someNUD_MouseWheel;

			#endregion

		}

		private void MaterialForm_Load(object sender, EventArgs e)
		{
			LanguageHelper.InitForm(this);

			Location = MousePosition;
			stepTemplate = mainForm.GetCurrentStepTemplate(); 
			string newLightType = mainForm.GetCurrentLightType();

			// 不论什么情况（但也只需在此刷新即可），都刷新《临时素材(复选项)》的Visible( 显示条件：①临时素材不为空，②其Mode与CurrentMode相同 )
			tempCB.Visible = mainForm.TempMaterialAst != null && mainForm.TempMaterialAst.Mode == mainForm.CurrentMode;

			// 灯具发生变化时，刷新tdTab(tdCB)
			if (oldLightType != newLightType)
			{
				refreshTdTab();
			}			
			// 灯具 或 模式发生变化时，刷新其余的Tab
			if (oldLightType != newLightType || oldMode != mainForm.CurrentMode)
			{
				refreshMaterialTab();
				refreshActionTab();
				refreshColorTab();				
			}			

			// 若mainForm的时间因子发生变化，则相应的步时间也发生变化
			if (mainForm.EachStepTime2 != oldEachStepTime)
			{
				// 动作步时间
				StNumericUpDown.Maximum = MainFormBase.MAX_StTimes * mainForm.EachStepTime2;
				StNumericUpDown.Minimum = mainForm.EachStepTime2; // 步时间如果为0，则毫无意义
				StNumericUpDown.Increment = mainForm.EachStepTime2;
				StNumericUpDown.Value = mainForm.EachStepTime2 * commonStepTime;

				// 颜色步时间	
				for (int panelIndex = 1; panelIndex < colorFLP.Controls.Count; panelIndex++)
				{
					NumericUpDown stNUD = (colorFLP.Controls[panelIndex] as Panel).Controls[0] as NumericUpDown;
					decimal oldValue = stNUD.Value / oldEachStepTime ;

					stNUD.Maximum = MainFormBase.MAX_StTimes * mainForm.EachStepTime2;
					stNUD.Minimum = mainForm.EachStepTime2; // 步时间如果为0，则毫无意义
					stNUD.Increment = mainForm.EachStepTime2;
					stNUD.Value = mainForm.EachStepTime2 * oldValue;
				}				
			}

			// 不论以下几个old变量是否发生了变动，都不影响soundStepTime可能发生变动；模式为1时，则有需要读取这个数据；
			if (mainForm.CurrentMode == 1)
			{
				IniHelper iniHelper = new IniHelper(mainForm.GlobalIniPath);
				soundStepTime = iniHelper.ReadInt("SK", mainForm.CurrentScene + "ST", 10);
			}

			oldMode = mainForm.CurrentMode ;
			oldLightType = newLightType ;
			oldEachStepTime = mainForm.EachStepTime2;
						
			materialTreeView.ExpandAll(); // 不论渲染成什么样，都要主动展开树，否则winForm会自己收纳起来。
			previewButton.Visible = mainForm.IsDeviceConnected & mainForm.CurrentMode == 0; // 音频模式就不要预览了，没有意义

			oneStepPlay(true);  //MaterialForm_Load：色块有可能发生变化，基于花销，直接传true(之前计划使用一个局部bool colorChanged，但可能会出现在预览过程中更改了色块，重新load时仍可能出现不匹配的问题)
		}		

		/// <summary>
		///  事件：关闭窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaterialForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			endView();
		}

		#region load时刷新各个素材来源相关的内容

		/// <summary>
		/// 辅助方法：刷新素材列表（最好不要有入参，而实时从主界面取出）
		/// </summary>
		private void refreshMaterialTab() {

			materialTreeView.Nodes.Clear();
			materialPath = IniHelper.GetSavePath() + @"\LightMaterial\" + (mainForm.CurrentMode == 0 ? "Normal" : "Sound");

			// 添加通用的素材
			string generalPath = materialPath + generalStr;
			if (Directory.Exists(generalPath))
			{
				TreeNode generalTreeNode = new TreeNode(LanguageHelper.TranslateSentence("通用素材"));
				string[] filePaths = Directory.GetFiles(generalPath);
				foreach (string filePath in filePaths)
				{
					FileInfo file = new FileInfo(filePath);
					string fileName = file.Name;
					if (fileName.EndsWith(".ini"))
					{
						TreeNode node = new TreeNode(
							fileName.Substring(0, fileName.IndexOf("."))
						);
						generalTreeNode.Nodes.Add(node);
					}
				}
				materialTreeView.Nodes.Add(generalTreeNode);
			}

			string lightType = mainForm.GetCurrentLightType();
			// 添加该灯的素材
			specialStr = @"\"  + lightType +  @"\";
			string specialPath = materialPath + specialStr;
			if (Directory.Exists(specialPath))
			{
				TreeNode specialTreeNode = new TreeNode(lightType );
				string[] filePaths = Directory.GetFiles(specialPath);
				foreach (string filePath in filePaths)
				{
					FileInfo file = new FileInfo(filePath);
					string fileName = file.Name;
					if (fileName.EndsWith(".ini"))
					{
						TreeNode node = new TreeNode(
							fileName.Substring(0, fileName.IndexOf("."))
						);
						specialTreeNode.Nodes.Add(node);
					}
				}
				materialTreeView.Nodes.Add(specialTreeNode);
			}
			materialTreeView.ExpandAll(); // 刷新后树会自动收起来
		}

		/// <summary>
		///  辅助方法：刷新动作Tab:根据mode及XY是否存在隐藏或显示；(步时间的更改不用放在这里)
		/// </summary>
		private void refreshActionTab() {
			Console.WriteLine("refreshActionTab");

			bool actionShow = mainForm.CurrentMode == 0 && StepWrapper.CheckXY(stepTemplate) ; 
			actionTab.Parent = actionShow ? tabControl1 : null ;
			actionCB.Visible = actionShow;
		}

		/// <summary>
		/// 辅助方法：刷新颜色Tab
		/// </summary>
		private void refreshColorTab() {
			
			if (StepWrapper.CheckRGB( stepTemplate) )
			{				
				colorTab.Parent = tabControl1;
				colorCB.Visible = true;

				// 当模式发生变化时，需要隐藏|显示 相关控件
				for (int panelIndex = 1; panelIndex < colorFLP.Controls.Count; panelIndex++)
				{
					(colorFLP.Controls[panelIndex] as Panel).Controls[0].Visible = mainForm.CurrentMode == 0;
					(colorFLP.Controls[panelIndex] as Panel).Controls[1].Visible = mainForm.CurrentMode == 0;
				}
				stLabel.Visible = mainForm.CurrentMode == 0;
				modeLabel.Visible = mainForm.CurrentMode == 0;

			}
			// 当并非RGB灯具时，直接隐藏快速调色功能
			else {
				colorTab.Parent = null;
				colorCB.Visible = false;				
			}
		}
		
		/// <summary>
		/// 辅助方法：刷新tdTab
		/// </summary>
		private void refreshTdTab()
		{
			tdComboBox.Items.Clear();
			foreach (TongdaoWrapper td in stepTemplate.TongdaoList)
			{
				tdComboBox.Items.Add(td.TongdaoName);
			}
			tdComboBox.SelectedIndex = 0;
		}

		#endregion

		/// <summary>
		/// 辅助方法：已连接且非预览中时,根据当前色块及固定通道，直接在灯具上显示颜色		
		/// </summary>
		private void oneStepPlay(bool colorChanged)
		{
			if (mainForm.IsDeviceConnected && !mainForm.IsPreviewing)
			{								
				// 一旦颜色模块发生任何变化，则处理singleColorMat：先设为null，再根据是否启用颜色来决定是否生成颜色块（）
				if (colorChanged) { 
					singleColorMat = null;
					if (colorTab.Parent != null && colorSelectedIndex > -1)
					{
						Panel colorPanel = colorFLP.Controls[colorSelectedIndex] as Panel;
						Color bColor = colorPanel.BackColor;

						//因为是单步数据，某些内容直接写死即可
						TongdaoWrapper[,] tdArray = new TongdaoWrapper[1, 4]; 
						tdArray[0, 0] = new TongdaoWrapper(dimmerStr, tgTrackBar.Value, 50);
						tdArray[0, 1] = new TongdaoWrapper(rStr, bColor.R, 50);
						tdArray[0, 2] = new TongdaoWrapper(gStr, bColor.G, 50);
						tdArray[0, 3] = new TongdaoWrapper(bStr, bColor.B, 50);

						singleColorMat = new MaterialAst
						{
							StepCount = 1,
							TdNameList = colorTdNameList,
							TongdaoArray = tdArray,
						};
					}
				}				
				MaterialAst singleMat  = MaterialAst.ProcessMaterialAst( singleColorMat , generateTdDict() , mainForm.CurrentMode );	 
				mainForm.OneStepPlay(singleMat);
				previewButton.Text = "预览"; 
			}
		}		

		/// <summary>
		///  事件：点击《预览|停止预览》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewButton_Click(object sender, EventArgs e)
		{
			if (!mainForm.IsDeviceConnected)
			{
				setNotice("尚未连接设备", true, true);
				return;
			}

			// 如果正在预览中，则停止预览（不需生成material）
			if (mainForm.IsPreviewing)
			{
				endView();
				// 停止预览后，恢复 单灯单步 
				oneStepPlay(true); //previewButton_Click() 内 停止预览(在预览过程中，可能更改了一些相应的颜色数据，所以最好的方式是直接传入true)
				setNotice("已停止预览。", false, true);
			}
			else if (generateComplexMaterial())
			{
				mainForm.PreviewButtonClick(complexMaterial);
				previewButton.Text = "停止预览"; 
				setNotice("正在预览复合素材...", false, true);
			}
		}

		/// <summary>
		/// 辅助方法：生成综合素材（1.素材 2.动作、颜色 3.固定通道）
		/// </summary>
		/// <returns></returns>
		private bool generateComplexMaterial()
		{
			// 先设为null，在后面的代码中如果没有任何素材被加入，则可以认定为无效素材
			complexMaterial = null;
			
			// 当临时素材不为空，且复选框勾选后，直接把complexMaterial赋为TempMaterialAst
			if (tempCB.Visible && tempCB.Checked) {
				complexMaterial = mainForm.TempMaterialAst;
			}

			// 生成选择的素材（保存的）
			if (materialCB.Checked)
			{
				string iniPath = getIniPath();
				if ( iniPath != null )
				{
					MaterialAst userMaterial = MaterialAst.GenerateMaterialAst( mainForm.CurrentMode,iniPath) ;
					complexMaterial = MaterialAst.ComplexMaterialAst(complexMaterial, userMaterial);
				}				
			}

			// 生成动作素材
			if ( generateActionMaterial() )
			{
				complexMaterial = MaterialAst.ComplexMaterialAst(complexMaterial, actionMaterial);				
			}

			// 生成调色素材（与动作同级，谁先谁后皆可）
			if (  generateColorMaterial() ){
				complexMaterial = MaterialAst.ComplexMaterialAst(complexMaterial, colorMaterial);
			}

			if (complexMaterial == null)
			{
				setNotice("请选择至少一个素材来源，且该来源数据非空。", true, true);
				return false;
			}
			else
			{				
				complexMaterial = MaterialAst.ProcessMaterialAst(complexMaterial , generateTdDict(), mainForm.CurrentMode );
				return true;
			}
		}
			
		/// <summary>
		/// 辅助方法：结束预览
		/// </summary>
		private void endView()
		{
			if (mainForm.IsDeviceConnected && mainForm.IsPreviewing)
			{
				mainForm.PreviewButtonClick(null);
				previewButton.Text = "预览";
				setNotice("已停止预览", false, true);
			}
		}
		
		/// <summary>
		/// 事件：点击《应用》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void useMaterialButton_Click(object sender, EventArgs e)
		{
			if (generateComplexMaterial()) {

				InsertMethod insMethod;
				Button btn = sender as Button;
				if (btn.Name == "insertButton")	{
					insMethod = InsertMethod.INSERT;
				}
				else if (btn.Name == "coverButton")
				{
					insMethod = clearCB.Checked ?InsertMethod.CLEAR_COVER: InsertMethod.COVER;
				}
				else
				{
					insMethod = InsertMethod.APPEND;
				}				
				mainForm.UseMaterial( complexMaterial , insMethod, false);				
				endView();// 使用素材后要关闭界面，若正在预览，需要主动结束预览(Hide不会主动触发FormClosed方法！)
				Hide();
			}			
		}

		#region 生成外部素材相关

		/// <summary>
		/// 事件：点击《刷新列表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void materialRefreshButton_Click(object sender, EventArgs e)
		{
			refreshMaterialTab( ) ; 
		}

		/// <summary>
		/// 事件：点击素材节点，确定要处理的素材
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void materialTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			materialTreeView.SelectedNode = e.Node;
			enableComplexButtons(); // materialTreeView_NodeMouseClick：不好判断发生了什么变化，故应该执行
		}

		/// <summary>
		/// 事件：点击《删除(素材)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void materialDeleteButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(
				LanguageHelper.TranslateSentence("确认删除素材") + "【" + materialTreeView.SelectedNode.FullPath + "】？",
				LanguageHelper.TranslateSentence("删除素材？"),
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Warning) == DialogResult.Cancel)
			{
				return;
			}

			string iniPath = getIniPath();
			if (iniPath != null)
			{
				// 1.删除文件
				try
				{
					File.Delete(iniPath);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}

				// 2.删除treeView1.SelectedNode;并设置ifJustDelete属性为true，避免用户误操作
				materialTreeView.SelectedNode.Remove();
				materialTreeView.SelectedNode = null; // 需主动设置为null，才不会选到被删节点的兄弟节点；但仍会选中第一个节点（如“通用”）
							
				enableComplexButtons(); //materialDeleteButton_Click：大概率发生变化，因为将选中项置为空了（连续删除不变）
			}		
		}

		/// <summary>
		///  辅助方法：供删除及使用素材使用，通过此方法可以直接获取选中项的物理路径
		/// </summary>
		/// <returns></returns>
		private string getIniPath()
		{
			// 1.先验证是否刚删除素材 或 空选
			if (materialTreeView.SelectedNode == null)
			{
				setNotice("请选择正确的素材名",true,true);
				return null;
			}

			//2. 验证是否子节点，父节点不是素材
			if (materialTreeView.SelectedNode.Level == 0)
			{
				setNotice("请选择素材树的子节点。",true,true);
				return null;
			}

			//3.验证素材名是否为空
			string materialName = materialTreeView.SelectedNode.Text;
			if (string.IsNullOrEmpty(materialName))
			{
				setNotice("素材名不得为空。",true,true);
				return null;
			}

			string astPath = materialTreeView.SelectedNode.Parent.Text.Equals(LanguageHelper.TranslateSentence("通用素材")) ? generalStr : specialStr;
			string iniPath = materialPath + astPath + materialName + ".ini";

			return iniPath;
		}
		
		#endregion

		#region 生成actionMaterial相关

		/// <summary>
		/// 辅助方法：生成动作的素材（供预览和使用动作）
		/// </summary>
		private bool generateActionMaterial()
		{
			if (actionCB.Visible && actionCB.Checked)
			{
				int tabIndex = actionTabControl.SelectedIndex;
				switch (tabIndex)
				{
					case 0: drawLine(); break;
					case 1: drawCircle(); break;
					case 2: drawSemicircle(); break;
					case 3: drawWave(); break;
					case 4: draw8(); break;
				}
				if (actionMaterial == null)
				{
					setNotice("生成动作数据出错，请重试", true, true);
					return false;
				}
				return true;
			}
			else {
				return false;
			}			
		}

		#region 画直线相关的方法：包括验证，生成数据等；

		/// <summary>
		///  辅助方法：制造画圆的图形
		/// </summary>
		private void drawLine()
		{
			int xValue = decimal.ToInt32(lineXNumericUpDown.Value);
			int y1Value = decimal.ToInt32(lineY1NumericUpDown.Value);
			int y2Value = decimal.ToInt32(lineY2NumericUpDown.Value);
			int phase = decimal.ToInt32(linePhaseNumericUpDown.Value) - 1;

			int stepCount = 4;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			// X轴值固定，无需处理相位
			tongdaoList[0, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);
			tongdaoList[1, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);
			tongdaoList[2, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);
			tongdaoList[3, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);

			tongdaoList[(0 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, y1Value, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, y1Value + y2Value / 2, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, y1Value + y2Value, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, y1Value + y2Value / 2, commonStepTime);

			actionMaterial = new MaterialAst
			{
				StepCount = stepCount,
				TdNameList = actionTdNameList,
				TongdaoArray = tongdaoList
			};
		}

		/// <summary>
		/// 事件：当Y1发生变化时，动态调整Y2的最大值（避免超过范围）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lineY1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int y1Value = decimal.ToInt32(lineY1NumericUpDown.Value);
			lineY2NumericUpDown.Maximum = 255 - y1Value;
		}

		/// <summary>
		/// 事件：更改了步时间之后，应该调整相应的stepTime
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lineStNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			commonStepTime = decimal.ToInt32(StNumericUpDown.Value / mainForm.EachStepTime2);

			// 需要在这里对波浪的倍率项进行限制
			waveTimesNumericUpDown.Maximum = 250 / commonStepTime;

		}

		#endregion

		#region 画圆及半圆相关的方法：包括验证，生成数据等；

		/// <summary>
		/// 辅助方法：生成圆的素材
		/// </summary>
		private void drawCircle()
		{
			int xValue = decimal.ToInt32(circleXNumericUpDown.Value);
			int yValue = decimal.ToInt32(circleYNumericUpDown.Value);
			int phase = decimal.ToInt32(circlePhaseNumericUpDown.Value) - 1;

			int stepCount = 4;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			tongdaoList[(0 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 0, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue / 2, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue / 2, commonStepTime);

			//　Y轴每步都一样，无需处理相位。
			tongdaoList[0, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[1, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[2, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[3, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);


			actionMaterial = new MaterialAst
			{
				StepCount = stepCount,
				TdNameList = actionTdNameList,
				TongdaoArray = tongdaoList
			};
		}

		/// <summary>
		/// 辅助方法：生成半圆的素材
		/// </summary>
		private void drawSemicircle()
		{
			int xValue = decimal.ToInt32(scXNumericUpDown.Value);
			int yValue = decimal.ToInt32(scYNumericUpDown.Value);
			int phase = decimal.ToInt32(scPhaseNumericUpDown.Value) - 1;

			int stepCount = 4;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			// 其实和圆的代码完全相同，区别只在于圆的X轴 转向需要超过170（而半圆必须小于170）
			tongdaoList[(0 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 0, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue / 2, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue / 2, commonStepTime);

			//Y轴每步都一样，无需处理相位。
			tongdaoList[0, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[1, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[2, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[3, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);

			actionMaterial = new MaterialAst
			{
				StepCount = stepCount,
				TdNameList = actionTdNameList,
				TongdaoArray = tongdaoList
			};
		}

		#endregion

		#region 画波浪相关的方法：包括验证，生成数据等；

		/// <summary>
		/// 生成波浪的素材
		/// </summary>
		private void drawWave()
		{
			int x1Value = decimal.ToInt32(waveX1NumericUpDown.Value);
			int x2Value = decimal.ToInt32(waveX2NumericUpDown.Value);
			int y1Value = decimal.ToInt32(waveY1NumericUpDown.Value);
			int y2Value = decimal.ToInt32(waveY2NumericUpDown.Value);
			int times = decimal.ToInt32(waveTimesNumericUpDown.Value);

			int stepCount = 2;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			tongdaoList[0, 0] = new TongdaoWrapper(xStr, x1Value, commonStepTime * times);
			tongdaoList[1, 0] = new TongdaoWrapper(xStr, x1Value + x2Value, commonStepTime * times);

			tongdaoList[0, 1] = new TongdaoWrapper(yStr, y1Value, commonStepTime);
			tongdaoList[1, 1] = new TongdaoWrapper(yStr, y1Value + y2Value, commonStepTime);

			actionMaterial = new MaterialAst
			{
				StepCount = stepCount,
				TdNameList = actionTdNameList,
				TongdaoArray = tongdaoList
			};

		}

		/// <summary>
		///  当《X轴初始位置》发生改变后，《X轴变化幅度》也要有所限制
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void waveX1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int x1Value = decimal.ToInt32(waveX1NumericUpDown.Value);
			waveX2NumericUpDown.Maximum = 255 - x1Value;
		}

		/// <summary>
		///  当《Y轴初始位置》发生改变后，《Y轴摆动幅度》也要有所限制
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void waveY1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int y1Value = decimal.ToInt32(waveY1NumericUpDown.Value);
			waveY2NumericUpDown.Maximum = 127 - y1Value;
		}

		#endregion

		#region 画8字相关方法：包括验证，生成数据等；

		/// <summary>
		/// 辅助方法：生成8字的素材
		/// </summary>
		private void draw8()
		{
			int yValue = decimal.ToInt32(eightYNumericUpDown.Value);
			int phase = decimal.ToInt32(eightPhaseNumericUpDown.Value) - 1;

			int stepCount = 4;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			tongdaoList[(0 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 0, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 85, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 85, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 0, commonStepTime);

			tongdaoList[(0 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, 255 - yValue, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, 255 - yValue, commonStepTime);

			actionMaterial = new MaterialAst
			{
				StepCount = stepCount,
				TdNameList = actionTdNameList,
				TongdaoArray = tongdaoList
			};

		}

		#endregion

		#endregion

		#region 生成colorMaterial相关

		/// <summary>
		/// 辅助方法：生成所有颜色组合的【素材】
		/// </summary>
		/// <returns></returns>
		private bool generateColorMaterial()
		{
			if (colorCB.Visible && colorCB.Checked)
			{
				if (colorCount == 0)
				{
					setNotice("尚未添加颜色块。", true, true);
					return false;
				}
				else
				{
					try
					{
						TongdaoWrapper[,] colorTongdaoArray = new TongdaoWrapper[colorCount, colorTdNameList.Count];
						for (int panelIndex = 1; panelIndex <= colorCount; panelIndex++)
						{
							Panel colorPanel = colorFLP.Controls[panelIndex] as Panel;

							// 常规模式，取控件内的值；音频模式，取固定值；
							int stepTime = mainForm.CurrentMode == 0 ? decimal.ToInt32((colorPanel.Controls[0] as NumericUpDown).Value / oldEachStepTime) : soundStepTime;
							int changeMode = mainForm.CurrentMode == 0 ? ((colorPanel.Controls[1] as CheckBox).Checked ? 1 : 0) : 1;

							colorTongdaoArray[panelIndex - 1, 0] = new TongdaoWrapper(dimmerStr, tgTrackBar.Value, stepTime, changeMode);
							colorTongdaoArray[panelIndex - 1, 1] = new TongdaoWrapper(rStr, colorPanel.BackColor.R, stepTime, changeMode);
							colorTongdaoArray[panelIndex - 1, 2] = new TongdaoWrapper(gStr, colorPanel.BackColor.G, stepTime, changeMode);
							colorTongdaoArray[panelIndex - 1, 3] = new TongdaoWrapper(bStr, colorPanel.BackColor.B, stepTime, changeMode);

							colorMaterial = new MaterialAst
							{
								StepCount = colorCount,
								TdNameList = colorTdNameList,
								TongdaoArray = colorTongdaoArray,
							};
						}
						return true;
					}
					catch (Exception ex)
					{
						setNotice("生成数据出错：" + ex.Message, true, false);
						return false;
					}
				}
			}
			else {
				return false;
			}	
		}

		/// <summary>
		/// 事件：点击《添加(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorAddButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.Cancel == myColorDialog.ShowDialog())
			{
				return;
			}

			Panel colorPanel = new Panel()
			{
				Size = colorPanelDemo.Size,
				Margin = colorPanelDemo.Margin,
				Visible = true,
				BackColor = myColorDialog.Color,
			};
			colorPanel.Click += colorPanel_Click;

			NumericUpDown stNUD = new NumericUpDown()
			{
				Font = stNUDDemo.Font,
				Location = stNUDDemo.Location,
				Size = stNUDDemo.Size,
				TextAlign = stNUDDemo.TextAlign,
				DecimalPlaces = stNUDDemo.DecimalPlaces,
				Maximum = stNUDDemo.Maximum * mainForm.EachStepTime2,
				Minimum = mainForm.EachStepTime2,
				Increment = mainForm.EachStepTime2,
				Value = mainForm.EachStepTime2 * commonStepTime,
				Visible = mainForm.CurrentMode == 0,
			};
			stNUD.MouseWheel += someNUD_MouseWheel;
			stNUD.KeyPress += stNUD_KeyPress;

			CheckBox cmCB = new CheckBox()
			{
				Font = cmCBDemo.Font,
				Location = cmCBDemo.Location,
				Size = cmCBDemo.Size,
				TextAlign = cmCBDemo.TextAlign,
				Text = cmCBDemo.Text,
				ForeColor = cmCBDemo.ForeColor,
				BackColor = cmCBDemo.BackColor,
				Visible = mainForm.CurrentMode == 0,
			};
			cmCB.KeyPress += cmCheckBox_KeyPress;

			colorPanel.Controls.Add(stNUD);
			colorPanel.Controls.Add(cmCB);

			colorFLP.Controls.Add(colorPanel);
			colorSelectedIndex = colorFLP.Controls.IndexOf(colorPanel);

			selectColorPanel(); //addButton_Click
			if (colorCount == 1)  enableComplexButtons(); // colorAddButton_Click；因为增加是逐一的，故当且仅当colorCount==1，表示从无到有
			
		}

		/// <summary>
		/// 事件：点击《修改(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorEditButton_Click(object sender, EventArgs e)
		{
			Color selectedColor = colorFLP.Controls[colorSelectedIndex].BackColor;
			myColorDialog.Color = selectedColor;  // 把选中色块的颜色，放给myColorDialog

			if (DialogResult.Cancel == myColorDialog.ShowDialog())
			{
				return;
			}
			colorFLP.Controls[colorSelectedIndex].BackColor = myColorDialog.Color;

			selectColorPanel(); //editButton_Click
		}

		/// <summary>
		/// 事件：点击《删除(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorDeleteButton_Click(object sender, EventArgs e)
		{
			colorFLP.Controls.RemoveAt(colorSelectedIndex);
			colorSelectedIndex = -1;
			selectColorPanel(); //colorDeleteButton_Click
			
			if (colorCount == 0)  enableComplexButtons(); // colorDeleteButton_Click；因为删除是逐一的，故当且仅当colorCount==0，表示从有到无
			
		}

		/// <summary>
		/// 事件：点击《清空(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorClearButton_Click(object sender, EventArgs e)
		{
			for (int panelIndex = colorFLP.Controls.Count - 1; panelIndex > 0; panelIndex--)
			{
				colorFLP.Controls.RemoveAt(panelIndex);
			}
			colorSelectedIndex = -1;
			selectColorPanel(); //clearButton_Click

			enableComplexButtons(); // colorClearButton_Click；colorCount必然发生有到无的变化
		}

		/// <summary>
		/// 事件：点击《色块(Panel)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorPanel_Click(object sender, EventArgs e)
		{
			// 当且仅当选中色块与内存内的数据不同时，才继续执行
			if ( colorSelectedIndex != colorFLP.Controls.IndexOf(sender as Panel)) {
				colorSelectedIndex = colorFLP.Controls.IndexOf(sender as Panel);
				selectColorPanel(); //colorPanel_Click：在此方法内有进行是否更改色块的判断
			}			
		}

		/// <summary>
		/// 辅助方法：更改selectedPanelIndex后，刷新相应的一些控件；并oneStepPlay(true)
		/// </summary>
		private void selectColorPanel()
		{
			astPanel.BackColor = colorSelectedIndex > 0 ? (colorFLP.Controls[colorSelectedIndex] as Panel).BackColor : Color.MintCream;
			astLabel.Text = colorSelectedIndex > 0 ? colorSelectedIndex + "" : "未选中步";
			editButton.Enabled = colorSelectedIndex > 0;
			colorDeleteButton.Enabled = colorSelectedIndex > 0;

			colorCount = colorFLP.Controls.Count - 1;
			clearButton.Enabled = colorCount > 0;

			oneStepPlay(true); //selectColorPanel()：所有调用selectColorPanel的方法，都有经过筛选，能保证一定是更改了色块，故传入true
		}
		
		/// <summary>
		/// 事件：(总)调光滑动杆值发生变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tgTrackBar_ValueChanged(object sender, EventArgs e)
		{
			TrackBar tBar = sender as TrackBar;

			tgNUD.ValueChanged -= tgNUD_ValueChanged;
			tgNUD.Value = tBar.Value;
			tgNUD.ValueChanged += tgNUD_ValueChanged;

			oneStepPlay(true); //tgTrackBar_ValueChanged
		}

		/// <summary>
		/// 事件：(总)调光nud的值发生变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tgNUD_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown nud = sender as NumericUpDown;

			tgTrackBar.ValueChanged -= tgTrackBar_ValueChanged;
			tgTrackBar.Value = decimal.ToInt32(nud.Value);
			tgTrackBar.ValueChanged += tgTrackBar_ValueChanged;

			oneStepPlay(true); //tgNUD_ValueChanged
		}

		/// <summary>
		/// 事件：《步时间NUD》的键盘点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void stNUD_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (colorCount > 1 && (e.KeyChar == 'a' || e.KeyChar == 'A'))
			{
				decimal unifySt = (sender as NumericUpDown).Value;

				// 用户点击取消，则return；否则继续往下走
				if (mainForm.IsNoticeUnifyTd)
				{
					if (DialogResult.Cancel == MessageBox.Show(
							LanguageHelper.TranslateSentence("确定要将所有步时间都设为") + "【" + unifySt + " S】?",
							LanguageHelper.TranslateSentence("统一步时间"),
							MessageBoxButtons.OKCancel,
							MessageBoxIcon.Question))
					{
						return;
					}
				}

				for (int controlIndex = 1; controlIndex < colorFLP.Controls.Count; controlIndex++)
				{
					((colorFLP.Controls[controlIndex] as Panel).Controls[0] as NumericUpDown).Value = unifySt;
				}
			}
		}

		/// <summary>
		/// 事件：《跳渐变复选框》的键盘点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmCheckBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (colorCount > 1 && (e.KeyChar == 'a' || e.KeyChar == 'A'))
			{
				bool unifyCM = (sender as CheckBox).Checked;
				string cmStr = unifyCM ? "渐变" : "跳变";

				// 用户点击取消，则return；否则继续往下走
				if (mainForm.IsNoticeUnifyTd)
				{
					if (DialogResult.Cancel == MessageBox.Show(
							LanguageHelper.TranslateSentence("确定要将所有跳渐变都设为【" + cmStr + "】吗?"),
							LanguageHelper.TranslateSentence("统一跳渐变"),
							MessageBoxButtons.OKCancel,
							MessageBoxIcon.Question))
					{
						return;
					}
				}

				for (int controlIndex = 1; controlIndex < colorFLP.Controls.Count; controlIndex++)
				{
					((colorFLP.Controls[controlIndex] as Panel).Controls[1] as CheckBox).Checked = unifyCM;
				}
			}
		}

		#endregion

		#region 生成固定通道(tdDict)相关

		/// <summary>
		/// 事件：更改tdCB的选中项（并因此定义右侧《添加》按键是否可用）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			enableTdAddButton();
		}		

		/// <summary>
		/// 事件：点击《添加（固定通道）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdAddButton_Click(object sender, EventArgs e)
		{
			for (int panelIndex = tdFLP.Controls.Count - 1; panelIndex > 0; panelIndex--)
			{
				if (tdFLP.Controls[panelIndex].Controls[0].Text.Contains(tdComboBox.Text))
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

			Label tdLabel = new Label
			{
				AutoSize = tdLabelDemo.AutoSize,
				Location = tdLabelDemo.Location,
				Size = tdLabelDemo.Size,
				Text = tdComboBox.Text,
			};

			NumericUpDown tdNUD = new NumericUpDown
			{
				Location = tdNUDDemo.Location,
				Maximum = tdNUDDemo.Maximum,
				Size = tdNUDDemo.Size,
				TextAlign = tdNUDDemo.TextAlign,
				Value = stepTemplate.TongdaoList[tdComboBox.SelectedIndex].ScrollValue,
			};
			tdNUD.ValueChanged += tdNUD_ValueChanged;
			tdNUD.MouseWheel += someNUD_MouseWheel;

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

			enableTdAddButton();
			oneStepPlay(false); //tdAddButton_Click() 
		}

		/// <summary>
		/// 事件：点击《删除（固定通道）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdDelButton_Click(object sender, EventArgs e)
		{
			tdFLP.Controls.Remove((sender as Button).Parent);
			enableTdAddButton();
			oneStepPlay(false); //tdDelButton_Click
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
			enableTdAddButton(); 
			oneStepPlay(false); //tdClearButton_Click
		}
		
		/// <summary>
		/// 辅助方法：更改了《固定通道》值 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdNUD_ValueChanged(object sender, EventArgs e)
		{
			oneStepPlay(false); //tdNUD_ValueChanged
		} 

		/// <summary>
		/// 辅助方法：生成固定通道列表;若不存在任何通道，则返回空的Dictionary（非null）
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

		/// <summary>
		/// 辅助方法：根据tdComboBox的选中项，确定添加按键是否可用
		/// </summary>
		private void enableTdAddButton()
		{
			tdAddButton.Enabled = true;
			for (int panelIndex = tdFLP.Controls.Count - 1; panelIndex > 0; panelIndex--)
			{
				if (tdFLP.Controls[panelIndex].Controls[0].Text.Contains(tdComboBox.Text))
				{
					tdAddButton.Enabled = false;
					return;
				}
			}
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

		/// <summary>
		/// 验证：对某些NumericUpDown进行鼠标滚轮的验证，避免一次性滚动过多
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someNUD_MouseWheel(object sender, MouseEventArgs e)
		{
			NumericUpDown nud = sender as NumericUpDown;
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = nud.Value + nud.Increment;
				if (dd <= nud.Maximum)
				{
					nud.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = nud.Value - nud.Increment;
				if (dd >= nud.Minimum)
				{
					nud.Value = dd;
				}
			}
		}

		/// <summary>
		///  验证：对某些TrackBar进行鼠标滚轮的验证，避免一次性滚动过多（与OS设置有关）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someTrackBar_MouseWheel(object sender, MouseEventArgs e)
		{

			TrackBar tb = sender as TrackBar;
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true; //设为true则屏蔽之后系统自行处理的操作（就是原来加3(Win10)之类的操作）
			}
			// 向上滚
			if (e.Delta > 0)
			{
				int dd = tb.Value + tb.SmallChange;
				if (dd <= tb.Maximum)
				{
					tb.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				int dd = tb.Value - tb.SmallChange;
				if (dd >= tb.Minimum)
				{
					tb.Value = dd;
				}
			}
		}
				
		#endregion
		
		/// <summary>
		///  事件：几个素材来源的复选框，[更改选中项]或者[隐藏显示事件]
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void materialCB_Changed(object sender, EventArgs e)
		{
			enableComplexButtons();  // materialCB_Changed：必然产生变化
		}

		/// <summary>
		/// 辅助方法：传入bool值，使能预览及应用按键；
		/// </summary>
		/// <param name="enable"></param>
		private void enableComplexButtons()
		{
			// 设定《删除(素材)》是否可用	
			bool materialSelected = materialTreeView.SelectedNode!=null && materialTreeView.SelectedNode.Level > 0;
			materialDeleteButton.Enabled = materialSelected ;

			// 计算《预览》、《应用》等是否可用
			bool enable = 
				(tempCB.Visible && tempCB.Checked)
				|| (materialCB.Checked && materialSelected) 
				|| (actionCB.Visible && actionCB.Checked ) 
				|| (colorCB.Visible && colorCB.Checked && colorCount >0 );

			previewButton.Enabled = enable;
			insertButton.Enabled = enable;
			coverButton.Enabled = enable;
			appendButton.Enabled = enable;
		}
	}
}
